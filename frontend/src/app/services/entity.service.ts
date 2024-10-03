import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Entity, DataEntity } from '../models/Entitys';

const endpoint ='https://localhost:7057/api/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class EntityService {

  constructor(private http:HttpClient) { }


  getAllEntities(): Observable<DataEntity> {
    return this.http.get<DataEntity>(endpoint+ 'Entity/'+'User/' +'GetAllEntityForUser' );
   
  }

  createEntity(entityData: any): Observable<any> {
    return this.http.post<any>(endpoint + 'Entity/CreateEntity', entityData, httpOptions);
  }

  updateEntity(entityData: any): Observable<any> {
    return this.http.put<any>(endpoint + 'Entity/UpdateEntity', entityData, httpOptions);
  }

  deleteEntity(entityId: string): Observable<boolean> {
    const url = (endpoint + 'Entity/DeleteEntity?entityId='+entityId);
    return this.http.delete<boolean>(url, httpOptions);
  }

}
