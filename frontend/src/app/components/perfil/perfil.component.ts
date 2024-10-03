import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { TinkService } from 'src/app/services/Tink/tink.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent {
 
  constructor(private formBuilder:FormBuilder, private router: Router, private tinkService: TinkService, private transactionService: TransactionsService){}
  panelOpenState = false;
  profileForm = this.formBuilder.group({
    name: ['',[Validators.required]],
   email: ['', [Validators.required, Validators.email]],
   password: ['', Validators.required],
   
    
  });
 
  TextForm= this.formBuilder.group({
    jsonAccount: [''],
   jsonTransactions: [''], 
  });



  saveForm(){
   console.log('Form data is ', this.profileForm.value, this.TextForm.value);
 }
 saveAccount() {
  if (this.TextForm) {
   
    let jsonAccount = this.TextForm.controls['jsonAccount'].value;
    

    if (jsonAccount) {
      const accounts = btoa(jsonAccount);
      this.transactionService.createJsonManualAccount(accounts).subscribe(
        (response:any)=> {



        },
      (error) => {
        console.error('Erro no acesso:', error);
        
      }


      )
      
    }
  }
}

saveTransaction() {
  if (this.TextForm) {
   
    let jsonTransaction = this.TextForm.controls['jsonTransactions'].value;
    

    if (jsonTransaction) {
      const transaction = btoa(jsonTransaction);
      this.transactionService.createJsonManualTransactions(transaction).subscribe(
        (response:any)=> {



        },
      (error) => {
        console.error('Erro no acesso:', error);
        
      }


      )
      
    }
  }
}
 ConectTink(){
    this.tinkService.getHttpTink().subscribe(
      (response: any) => {
        
        
        window.location.href=response;
        //this.router.navigateByUrl("https://www.google.pt");
      },
      (error) => {
        console.error('Erro no acesso:', error);
        
      }
      
    );

 }
 }
