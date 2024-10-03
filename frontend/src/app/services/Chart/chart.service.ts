import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

const endpoint = 'https://localhost:7057/api/Chart/';

@Injectable({
  providedIn: 'root'
})
export class ChartService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  getDataForMonthInYear(): Observable<Object> {
    return this.http.get<Object>(endpoint + 'GetDataForMonthInYear', this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any): Observable<never> {
    console.error('Ocorreu um erro:', error);
    return throwError('Erro ao obter dados do servidor.');
  }
}
