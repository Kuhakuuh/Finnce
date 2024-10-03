import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { UserService } from 'src/app/services/User/user.service';

@Component({
  selector: 'app-registo-conta',
  templateUrl: './registo-conta.component.html',
  styleUrls: ['./registo-conta.component.css'],

})
export class RegistoContaComponent {
  constructor(private formBuilder:FormBuilder,private toast:NgToastService, private router: Router, private userService: UserService){}

  profileForm = this.formBuilder.group({
   email: ['', [Validators.required, Validators.email]],
   password: ['', Validators.required],
   passwordConfirm: ['', Validators.required],
   telefone: ['', Validators.required],
   nome: ['', Validators.required],
   username:['', Validators.required],
   dataNascimento:  [ Validators.required,this.validateDate.bind(this)],
  });
  saveForm(){
    console.log('Form data is ', this.profileForm.value);
  }

  creatUser(){

    const newUserData = {
      
      "name": this.profileForm.value.nome,
      "userName": this.profileForm.value.username,
      "email": this.profileForm.value.email,
      "password": this.profileForm.value.password,
      "phone": this.profileForm.value.telefone,
      "birthDate": this.profileForm.value.dataNascimento
    };
    console.log("criei o user")
    this.userService.createUser(newUserData).subscribe(
      response => 
      {
        this.profileForm.reset();
        this.toast.success({detail:"SUCCESS",summary:"Registo com sucesso",sticky:false, position: 'topRight',duration:2000});
        this.router.navigate(['/login']);
        
      },
      error => {
        console.error('Erro ao criar conta', error);
    
      }
    )
    this.profileForm.reset();
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
