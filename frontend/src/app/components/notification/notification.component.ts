import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NotificationService } from 'src/app/services/notification.service';
import { SignalrService } from 'src/app/services/signalr.service';
import { Notification } from 'src/app/models/Notification';
@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {
  NotificationArray: Array<Notification> = [];
  displayedTable: string[] = ['Name', 'Message','Delete'];
  constructor(private notificationService: NotificationService)
  {
    
  }

  ngOnInit(): void {
    this.getAllNotifications();
  }
  
  getAllNotifications() {
    this.notificationService.getAllNotifications().subscribe(
      (data: any) => {
        this.NotificationArray = data.result.$values ? data.result.$values : [data]; 
        
      },
      (error) => {
        console.error('Erro ao obter notificações:', error);
      }
    );
  }

  deleteNotification(id: string){
     this.notificationService.deleteNotification(id).subscribe(result => {
    window.location.reload();
  },
  error => {
    console.error('Erro ao eliminar notificação', error);
  })

  }
  
  
  
  
  }