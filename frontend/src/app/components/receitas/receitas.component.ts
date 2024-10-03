import { Component, OnInit } from '@angular/core';
import { Transaction } from 'src/app/models/Transaction';
import { TransactionsService } from 'src/app/services/transactions.service';
import { SuccessMessageComponent } from '../success-message/success-message.component';
import { Account } from 'src/app/models/Accounts';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountsService } from 'src/app/services/accounts.service';
import { MatDialog } from '@angular/material/dialog';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/Category';
import { formatDate } from '@angular/common';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-receitas',
  templateUrl: './receitas.component.html',
  styleUrls: ['./receitas.component.css']
})
export class ReceitasComponent implements OnInit {
  revenueArray: Array<Transaction> = [];
  transactionArray: Array<Transaction> = [];
  contasArray:Array<Account> = [];
  AddRevenueForm: FormGroup;
  categoriesArray:Array<Category> = [];

  constructor(private transactionService: TransactionsService,
    private accountService:AccountsService,
    private form: FormBuilder,
    private toast: NgToastService,
    private categoryService:CategoryService,
    private dialog: MatDialog) { 
      this.AddRevenueForm = this.form.group({
        value: [ Validators.required],
        date: [ Validators.required,this.validateDate.bind(this)],
        decription:[ ],
        conta: [ Validators.required],
        category: [ Validators.required]
      });
  }

  ngOnInit(): void {
    this.getRevenues();
    this.getAllAccounts();
    this.getAllCategories();
  }

  getRevenues(){
    this.transactionService.getAllUserTransactions().subscribe(
      (data: any) => {
        this.transactionArray = data.$values ? data.$values : [data]; 
        this.revenueArray=this.transactionArray.filter(transaction => transaction.amount>0)
        this.revenueArray.sort(
          (a, b) => new Date(b.dateBokeed).getTime() - new Date(a.dateBokeed).getTime()
        );
        console.log('Revenue:', this.revenueArray);
        //console.log('Transactions:', this.transactionArray);
      },
      (error) => {
        console.error('Erro ao obter transactions:', error);
      }); 
  }
  
  totalValue():number{
    var result:number =0;
    this.revenueArray.forEach((transacoes)=>{
      result += transacoes.amount;
    })
    return result;
  }
  
  lastRevenue(): Transaction[] {
    if (this.revenueArray && this.revenueArray.length > 5) {
      return this.revenueArray.slice(0,5);
    }
    return this.revenueArray;
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
      this.categoriesArray = data.result.$values ? data.result.$values : [data]; 
      console.log('Categories:', this.categoriesArray);
    },
    (error) => {
      console.error('Erro ao obter categories:', error);
    }
  );
}
createRevenue(){
  const newTransactionData = {
    "IdAccount": this.AddRevenueForm.value.conta.id, 
    "Amount": this.AddRevenueForm.value.value,
    "DescriptionDisplay": this.AddRevenueForm.value.decription,
    "DateBokeed": this.AddRevenueForm.value.date,
    "CurrencyCode":0,
    "IdUser":this.AddRevenueForm.value.conta.idUser, 
    "Type":"Revenue",
    "IdCategory":this.AddRevenueForm.value.category.id
    
  };
  //console.log(newTransactionData)
  this.transactionService.createManualTransaction(newTransactionData).subscribe(response => {
      //this.getExpenses();

      this.AddRevenueForm.reset();
      this.toast.success({ detail: "SUCCESS", summary: "Receita Adiciona!", sticky: false, position: 'topRight', duration: 2000 });
      setTimeout(() => {
        location.reload();
       
      }, 2000);
    },
    error => {
      console.error('Erro ao criar conta', error);
    }
)}


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
}
