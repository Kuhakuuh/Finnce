
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Accounts } from 'src/app/models/Accounts';



const endpoint ='https://localhost:7057/api/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};
@Injectable({
  providedIn: 'root'
})
export class TinkService {

  constructor(private http:HttpClient) { }

  getHttpTink(): Observable<string> {
    return this.http.post(endpoint + 'TinkUser/CreateUserTink', "", { responseType: 'text' });
  }
  
}
