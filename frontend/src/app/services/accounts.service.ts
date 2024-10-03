import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transaction } from '../models/Transaction';
import { Accounts } from '../models/Accounts';


const endpoint ='https://localhost:7057/api/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};
@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  constructor(private http:HttpClient) { }

  getAllAccounts(): Observable<Accounts> {
    return this.http.get<Accounts>(endpoint+ 'Account/'+'User/' +'GetAllAccountsForUser' );

  }

  createAccount(accountData: any): Observable<any> {
    return this.http.post<any>(endpoint + 'Account/CreateAccount', accountData, httpOptions);
  }
}
