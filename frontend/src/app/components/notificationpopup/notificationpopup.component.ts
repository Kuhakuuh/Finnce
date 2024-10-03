import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { Notification } from 'src/app/models/Notification';
@Component({
  selector: 'app-notification-pop-up',
  templateUrl: './notificationpopup.component.html',
  styleUrls: ['./notificationpopup.component.css']
})
export class NotificationPopUpComponent implements OnInit{
  private _hubConnection!: HubConnection;
  constructor(private toastr: ToastrService) {}
  ngOnInit(): void {
    this._hubConnection =new HubConnectionBuilder().withUrl('https://localhost:7057/Notify',
   { skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets}).build();

    this._hubConnection
      .start()
      .then(() =>{ console.log('Connection started!');  this._hubConnection.invoke("Transaction", "1") })
      .catch(err => console.log('Error while establishing connection :('));
    this._hubConnection.on('SendMessage', (notification: Notification) => {
      this.showSuccess(notification);
    });

  }

  showSuccess(notification: Notification) {
    this.toastr.success(notification.name+notification.message, notification.message,{ positionClass: 'toast-top-right'});
  }
}
