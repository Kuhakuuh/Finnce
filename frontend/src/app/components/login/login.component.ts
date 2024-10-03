import { Token } from '@angular/compiler';
import {Component} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/services/Guards/auth.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  
})

export class LoginComponent {
  
  constructor(private formBuilder:FormBuilder, private router: Router, private authService : AuthService,private toast:NgToastService){}

 profileForm = this.formBuilder.group({
  email: ['', [Validators.required]],
  password: ['', Validators.required],
   
 });



login() {
  const email = this.profileForm.get('email')?.value;
  const password = this.profileForm.get('password')?.value;

  if (email && password) {
    this.authService.login(email, password).subscribe(
      (response) => {
        
        
        this.router.navigate(['dashboards']);
        this.toast.success({detail:"SUCCESS",summary:"Login com sucesso",sticky:false, position: 'topRight',duration:2000});
        
      },
      (error) => {
        console.error('Erro na autenticação:', error);
        this.router.navigate(['Login']);
      }
      
    );
  }
}



routerForm(){
  this.router.navigate(['/registo-conta']);
}
}