import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataNotification } from '../models/Notification';

const endpoint ='https://localhost:7057/api/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private http:HttpClient) { }


  getAllNotifications(): Observable<DataNotification> {
    return this.http.get<DataNotification>(endpoint+ 'Notification/'+'User/' +'GetAllNotificationsForUser' );
    
   
  }

  createNotifications(entityData: any): Observable<any> {
    return this.http.post<any>(endpoint + 'Notification/CreateNotification', entityData, httpOptions);
  }


  deleteNotification(notificationId: string): Observable<boolean> {
    const url = (endpoint + 'Notification/DeleteNotification?notificationId='+notificationId);
    return this.http.delete<boolean>(url, httpOptions);
  }
}
