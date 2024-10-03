import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, map } from 'rxjs';
import { AuthStorageService } from './auth-storage.service';
import { AuthResponse } from 'src/app/models/AuthResponse';


const endpoint ='https://localhost:7057/api/Authenticate/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  
  constructor(private http:HttpClient, private authStorageService: AuthStorageService) { }



  login(userName: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(
      endpoint + 'login',
      {
        userName,
        password,
      },
      httpOptions
    )
    .pipe(
      map(response => {
        this.authStorageService.saveUser(response.token);
        
        return response;
      }),
      catchError(error => {
        throw error;
      })
      
    );
  }



  
  clean(): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      try {
        window.sessionStorage.removeItem('token');
        window.sessionStorage.clear();
        resolve();
      } catch (error) {
        reject(error);
      }
    });
  }
}
