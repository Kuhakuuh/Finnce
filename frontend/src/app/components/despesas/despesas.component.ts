import { Component, OnDestroy, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Subject, takeUntil, window } from 'rxjs';
import { TransactionsService } from 'src/app/services/transactions.service';
import { Transaction } from 'src/app/models/Transaction';
import { AccountsService } from 'src/app/services/accounts.service';
import { Account } from 'src/app/models/Accounts';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/Category';
import { formatDate } from '@angular/common';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-despesas',
  templateUrl: './despesas.component.html',
  styleUrls: ['./despesas.component.css'],
})
export class DespesasComponent implements OnDestroy, OnInit {
  expenseArray: Array<Transaction> = [];
  transactionArray: Array<Transaction> = [];
  contasArray: Array<Account> = [];
  AddExpenseForm: FormGroup;
  categoriesArray: Array<Category> = [];
  notCategorizedExpenses: Array<Transaction> = [];
  editingMode = false;
  transaction!: Transaction;
  updateCategory!: Category;
  categoryId?: string;

  displayedTable: string[] = ['description', 'amount', 'date', 'editar'];
  ngOnInit(): void {
    this.getAllAccounts();
    this.getAllCategories();
    this.getExpenses();
    //this. getnotCategorizedExpenses();
  }

  getExpenses() {
    this.transactionService.getAllUserTransactions().subscribe(
      (data: any) => {
        this.transactionArray = data.$values ? data.$values : [data];
        this.expenseArray = this.transactionArray.filter(
          (transaction) => transaction.amount < 0
        );
        this.expenseArray.sort(
          (a, b) =>
            new Date(b.dateBokeed).getTime() - new Date(a.dateBokeed).getTime()
        );
        console.log('expenses:', this.expenseArray);
        this.updateNotCategorized();
      },
      (error) => {
        console.error('Erro ao obter transactions:', error);
      }
    );
  }

  updateNotCategorized() {
    this.expenseArray.forEach((expense) => {
      if (expense.idCategory === this.categoryId) {
        this.notCategorizedExpenses.push(expense);
      }
    });
    console.log('notCategorized', this.notCategorizedExpenses);
  }
  destroyed = new Subject<void>();
  currentScreenSize: string | undefined;

  // Create a map to display breakpoint names for demonstration purposes.
  displayNameMap = new Map([
    [Breakpoints.XSmall, 'XSmall'],
    [Breakpoints.Small, 'Small'],
    [Breakpoints.Medium, 'Medium'],
    [Breakpoints.Large, 'Large'],
    [Breakpoints.XLarge, 'XLarge'],
  ]);

  constructor(
    private dialog: MatDialog,
    private form: FormBuilder,
    private transactionService: TransactionsService,
    private toast: NgToastService,
    private accountService: AccountsService,
    breakpointObserver: BreakpointObserver,
    private categoryService: CategoryService,
    private router: Router
  ) {
    this.AddExpenseForm = this.form.group({
      value: [Validators.required],
      date: [Validators.required, this.validateDate.bind(this)],
      decription: [],
      conta: [Validators.required],
      category: [Validators.required],
    });
    breakpointObserver
      .observe([
        Breakpoints.XSmall,
        Breakpoints.Small,
        Breakpoints.Medium,
        Breakpoints.Large,
        Breakpoints.XLarge,
      ])
      .pipe(takeUntil(this.destroyed))
      .subscribe((result) => {
        for (const query of Object.keys(result.breakpoints)) {
          if (result.breakpoints[query]) {
            this.currentScreenSize =
              this.displayNameMap.get(query) ?? 'Unknown';
          }
        }
      });
  }
  ngOnDestroy(): void {
    this.destroyed.next();
    this.destroyed.complete();
  }

  totalValue(): number {
    var result: number = 0;
    this.expenseArray.forEach((transacoes) => {
      result += transacoes.amount;
    });
    return result;
  }

  lastExpenses(): Transaction[] {
    if (this.expenseArray && this.expenseArray.length > 5) {
      return this.expenseArray.slice(0, 5);
    }
    return this.expenseArray;
  }

  getAllAccounts() {
    this.accountService.getAllAccounts().subscribe(
      (data: any) => {
        this.contasArray = data.$values ? data.$values : [data];
        console.log('Contas:', this.contasArray);
      },
      (error) => {
        console.error('Erro ao obter contas:', error);
      }
    );
  }
  getAllCategories() {
    this.categoryService.getAllCategories().subscribe(
      (data: any) => {
        this.categoriesArray = data.result.$values
          ? data.result.$values
          : [data];
        console.log('Categories:', this.categoriesArray);
        this.categoriesArray.forEach((category) => {
          if (category.name == 'Categoria Genérica') {
            this.categoryId = category.id;
            console.log('CategoryId:', this.categoryId);
          }
        });
      },
      (error) => {
        console.error('Erro ao obter categories:', error);
      }
    );
  }

  createExpense() {
    const newTransactionData = {
      IdAccount: this.AddExpenseForm.value.conta.id,
      Amount: this.AddExpenseForm.value.value * -1,
      DescriptionDisplay: this.AddExpenseForm.value.decription,
      DateBokeed: this.AddExpenseForm.value.date,
      CurrencyCode: 0,
      IdUser: this.AddExpenseForm.value.conta.idUser,
      Type: 'Expense',
      IdCategory: this.AddExpenseForm.value.category.id,
    };
    console.log(newTransactionData);
    this.transactionService
      .createManualTransaction(newTransactionData)
      .subscribe(
        (response) => {
          //this.getExpenses();
          this.AddExpenseForm.reset();
          this.toast.success({
            detail: 'SUCCESS',
            summary: 'Despesa Adiciona!',
            sticky: false,
            position: 'topRight',
            duration: 2000,
          });
          this.updateNotCategorized();
        },
        (error) => {
          console.error('Erro ao criar conta', error);
        }
      );
  }

  update() {
    if (this.transaction != null) {
      const newTransactionData = {
        id: this.transaction.id,
        type: 0,
        descriptionDisplay: this.transaction.descriptionDisplay,
        amount: this.transaction.amount,
        currencyCode: 0,
        dateBokeed: this.transaction.dateBokeed,
        idCategory: this.updateCategory.id,
        idAccount: this.transaction.idAccount,
      };
      console.log(newTransactionData);

      this.transactionService.updateTransaction(newTransactionData).subscribe(
        (response) => {
          console.log('Categoria atualizada com sucesso', response);
          this.toast.success({
            detail: 'SUCCESS',
            summary: 'Categoria Atualizada!',
            sticky: false,
            position: 'topRight',
            duration: 2000,
          });
          this.updateNotCategorized();
        },
        (error) => {
          console.error('Erro ao editar categoria', error);
        }
      );
      this.editingMode = false;
    } else {
      this.canceledit();
    }
    this.updateNotCategorized();
  }

  formatDisplayDate(dateString: string): string {
    const date = new Date(dateString);
    if (!isNaN(date.getTime())) {
      return formatDate(date, 'dd/MM/yyyy', 'en-US');
    } else {
      return 'Invalid Date';
    }
  }

  validateDate(control: any) {
    const selectedDate = new Date(control.value);
    const currentDate = new Date();
    if (selectedDate > currentDate) {
      return { invalidDate: true };
    }
    return null;
  }

  edit(transaction: Transaction) {
    this.editingMode = true;
    this.transaction = transaction;
  }

  canceledit() {
    this.editingMode = false;
  }
}
