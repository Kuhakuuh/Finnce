import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { Notification } from '../models/Notification'; 
import { MatDialog } from '@angular/material/dialog';
import { NotificationDialogComponent } from '../components/notification-dialog/notification-dialog.component';
import { NgToastService } from 'ng-angular-popup';
@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection!: signalR.HubConnection;

  constructor( private dialog: MatDialog,private toast: NgToastService) {

  }
    public startConnection = () => {
      this.hubConnection = new signalR.HubConnectionBuilder()
                              .withUrl('https://localhost:7057/Notify',{ skipNegotiation: true,
                              transport: signalR.HttpTransportType.WebSockets})
                              .build();
      this.hubConnection
        .start()
        .then(() => console.log('Connection started'))
        .catch(err => console.log('Error while starting connection: ' + err))
    }
    
    public addTransactionListner = () => {
      this.hubConnection.on('SendMessage', (notification: Notification) => {
        this.showNotification(notification);
        
      });
    }

    public showNotification(notification: Notification) {
      this.toast.info({detail:"INFO",summary:notification.message,sticky:false, position: 'topRight',duration:2000});

    }

    openNotification(description: string, message: string): void {
      const dialogRef = this.dialog.open(NotificationDialogComponent, {
        width: '300px',
        position: { top: '20px', right: '20px' }, // Posicionamento no canto superior direito
        data: { description, message },
      });
  
      setTimeout(() => {
        dialogRef.close(); // Fecha o dialog após um tempo (opcional)
      }, 5000); // Tempo em milissegundos (neste caso, 5 segundos)
    }
    

}