
import { Injectable } from '@angular/core';
import { AuthResponse } from 'src/app/models/AuthResponse';
const USER_KEY = 'auth-user';


@Injectable({
  providedIn: 'root'
})
export class AuthStorageService {
  
  constructor() { }

  clean(): void {
    sessionStorage.clear()
    //window.sessionStorage.clear();
    
  }



  public saveUser(Token: any): void {
    window.sessionStorage.removeItem(Token);
    window.sessionStorage.setItem('token',Token);
  }

getToken(): string | null {
  const tokenString = window.sessionStorage.getItem('token');

  if (tokenString !== null) {
    try {
      
      return tokenString;
    } catch (error) {
      console.error('Error parsing token:', error);
      return null;
    }
  } else {
    return null;
  }
}


 
  
  public isLoggedIn(): boolean {
    const token = window.sessionStorage.getItem('token');
    if (token) {
      return true;
    }

    return false;
  }
}
