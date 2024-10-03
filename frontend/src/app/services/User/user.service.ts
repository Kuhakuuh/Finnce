import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
const endpoint ='https://localhost:7057/api/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }


  createUser(userData: any): Observable<any> {
    return this.http.post<any>(endpoint + 'User/CreateUser', userData, httpOptions);
  }
  
}
