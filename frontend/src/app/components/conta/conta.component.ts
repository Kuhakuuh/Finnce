import { Component } from '@angular/core';
import { AccountsService } from '../../services/accounts.service'; 
import { Account } from '../../models/Accounts';
import { Accounts } from '../../models/Accounts';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-conta',
  templateUrl: './conta.component.html',
  styleUrls: ['./conta.component.css'],
  

})
export class ContaComponent {
  showBox = false;

  tipos: string[] = [
    
    'Carteira',
    'Poupança',
  ];
  displayedColumns: string[] = ['name', 'description', 'typeAccount','amount'];

  contas: Accounts | undefined;
  contasArray: Array<Account> = [];
  AddAcountFormulary: FormGroup;

  contasObject: Accounts | undefined;
  constructor(private accountsService: AccountsService, private router: Router, private fb: FormBuilder) {
     this.getAllAccounts();
     this.AddAcountFormulary = this.fb.group({
      tipoConta: ['' , Validators.required],
      nome: ['', Validators.required],
      descricao: ['', Validators.required],
      valor: ['', Validators.required]
    });
  }

  changeSlide(){
      this.showBox=!this.showBox
  }

  criarConta() {
    const newAccountData = {
      
      "name": this.AddAcountFormulary.value.nome,
      "amount": this.AddAcountFormulary.value.valor,
      "typeAccount": this.AddAcountFormulary.value.tipoConta,
      "description": this.AddAcountFormulary.value.descricao,
      "statusBlockedTransation": false
    };
    
    this.accountsService.createAccount(newAccountData).subscribe(
      response => 
      {
        this.getAllAccounts();
        this.AddAcountFormulary.reset();
      },
      error => {
        console.error('Erro ao criar conta', error);
    
      }
    )
    this.AddAcountFormulary.reset();
  }
  getAllAccounts() {
   
    this.accountsService.getAllAccounts().subscribe(
      (data: any) => {
        this.contasArray = data.$values ? data.$values : [data]; 
        
      },
      (error) => {
        console.error('Erro ao obter contas:', error);
      }
    );
}
}