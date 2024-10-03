import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category, DataCategory } from '../models/Category';

const endpoint ='https://localhost:7057/api/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};
@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient) { }


  getAllCategories(): Observable<DataCategory> {
    return this.http.get<DataCategory>(endpoint+ 'Category/'+'User/' +'GetAllCategoryForUser' );
   
  }

  createCategory(categoryData: any): Observable<any> {
    return this.http.post<any>(endpoint + 'Category/CreateCategory', categoryData, httpOptions);
  }

}
