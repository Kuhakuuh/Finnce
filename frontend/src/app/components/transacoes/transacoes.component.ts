import { Component } from '@angular/core';
import { TransactionsService } from 'src/app/services/transactions.service';
import { DataTransaction, Transaction } from 'src/app/models/Transaction';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/Category';

@Component({
  selector: 'app-transacoes',
  templateUrl: './transacoes.component.html',
  styleUrls: ['./transacoes.component.css'],
})
export class TransacoesComponent {
  TransactionArray: Array<Transaction> = [];
  DisplayTransactionArray: Array<Transaction> = [];
  categoriesArray:Array<Category> = [];
  descriptionTransaction = '';
  tipoConta = '';
  categoria:Category | undefined;
  tipos: string[] = ['Todas', 'Despesas', 'Receitas'];
  
  ngOnInit(): void {
    this.getUserTransactions();
    this.getAllCategories();
  }
  
 constructor( 
  private transactionServices:TransactionsService,
  private categoryService:CategoryService,){
 }

 getUserTransactions(){
  this.transactionServices.getAllUserTransactions().subscribe(
    (data: any) => {
      this.TransactionArray = data.$values ? data.$values : [data];
      this.DisplayTransactionArray = data.$values ? data.$values : [data];  
      this.TransactionArray.sort(
        (a, b) => new Date(b.dateBokeed).getTime() - new Date(a.dateBokeed).getTime()
      );
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
      },
      (error) => {
        console.error('Erro ao obter categories:', error);
      }
    );
  }

  displayedTable: string[] = ['description', 'amount', 'date'];

  clear(){
    location.reload();
    this.descriptionTransaction = '';
    this.tipoConta = '';
    this.categoria = undefined;
  }

  pesquisa() {
    let atualizarDisplayTransactionArray: Array<Transaction> = [];
    console.log('ID Transação:', this.descriptionTransaction);
    console.log('Tipo de Conta:', this.tipoConta);
    let tipo: string | number | undefined=undefined
    if(this.tipoConta=="Despesas"){
      tipo=1
    }else if(this.tipoConta=="Receitas"){
      tipo=0
    }else{
      tipo=-1
    }
   
    if(this.descriptionTransaction!=''||this.tipoConta!=''||this.categoria!=undefined){

      //pesqusia pelo nome, tipo e categoria
    if(this.descriptionTransaction!='' && tipo != -1 && this.categoria != undefined){
      this.TransactionArray.forEach((item) => {
        if (item.descriptionDisplay?.includes(this.descriptionTransaction) && item.type == tipo && item.idCategory == this.categoria?.id ) {
          atualizarDisplayTransactionArray.push(item);
          
        }
      });
    }
    //pesquia pelo nome e tipo
    else if (this.descriptionTransaction!='' && tipo !=-1  && this.categoria == undefined ){
      this.TransactionArray.forEach((item) => {
        if (item.descriptionDisplay?.includes(this.descriptionTransaction) && item.type == tipo ) {
          atualizarDisplayTransactionArray.push(item);
        }
      });
    }
    //pesquisa pelo nome e categoria
    else if (this.descriptionTransaction!='' && tipo == -1  && this.categoria != undefined ){
      this.TransactionArray.forEach((item) => {
        if (item.descriptionDisplay?.includes(this.descriptionTransaction) && item.idCategory==this.categoria?.id ) {
          atualizarDisplayTransactionArray.push(item);
        }
      });
    }
    //pesquisa pelo nome
    else if (this.descriptionTransaction!='' && tipo == -1  && this.categoria == undefined ){
      this.TransactionArray.forEach((item) => {
        if (item.descriptionDisplay?.includes(this.descriptionTransaction) ) {
          atualizarDisplayTransactionArray.push(item);
        }
      });
    }
    // pesquisa pelo tipo e categoria
    else if(this.descriptionTransaction=='' && tipo != -1  && this.categoria != undefined ){
      this.TransactionArray.forEach((item) => {
        if (item.type == tipo && item.idCategory == this.categoria?.id ) {
          atualizarDisplayTransactionArray.push(item);
        }
      });
    }
    //pesquisa pelo tipo 
    else if(this.descriptionTransaction=='' && tipo != -1  && this.categoria == undefined ){
      this.TransactionArray.forEach((item) => {
        if (item.type == tipo) {
          atualizarDisplayTransactionArray.push(item);
        }
      });
    }
     //pesquisa pelo tipo "Todas"
     else if(this.descriptionTransaction=='' && tipo == -1  && this.categoria == undefined ){
      this.TransactionArray.forEach((item) => {
          atualizarDisplayTransactionArray.push(item);
      });
    }
    //pesquisa so pela categoria
    else if(this.descriptionTransaction=='' && tipo == -1  && this.categoria !=undefined ){
      this.TransactionArray.forEach((item) => {
        if (item.idCategory == this.categoria?.id) {
          atualizarDisplayTransactionArray.push(item);
        }
      });
    }
      
    this.DisplayTransactionArray=atualizarDisplayTransactionArray
  }
    }
    
}

