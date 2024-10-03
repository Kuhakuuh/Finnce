
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataTransaction, Transaction } from '../models/Transaction';

const endpoint ='https://localhost:7057/api/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};


@Injectable({
  providedIn: 'root'
})

export class TransactionsService {

  constructor(private http:HttpClient) { }

  getAllUserTransactions(): Observable<DataTransaction> {
    return this.http.get<DataTransaction>(endpoint+ 'Transations/'+'User/' +'GetAllTransactionsOfUser' );
    
  }

  updateTransaction(transactionData: any): Observable<any> {
    return this.http.post<Transaction>(endpoint + 'Transations/EditTransaction', transactionData, httpOptions);
  }

  createManualTransaction(transactionData: any): Observable<any> {
    return this.http.post<any>(endpoint + 'Transations/CreateManualTransaction', transactionData, httpOptions);
  }


  createJsonManualAccount(accounts: string): Observable<any>{
    return this.http.post<any>(endpoint + 'Transations/RegisterJsonManualAccounts',  `"${accounts}"`, httpOptions);

  }

  createJsonManualTransactions(transactions: string): Observable<any>{
    return this.http.post<any>(endpoint + 'Transations/RegisterJsonManualTransactions',  `"${transactions}"`, httpOptions);

  }
}
