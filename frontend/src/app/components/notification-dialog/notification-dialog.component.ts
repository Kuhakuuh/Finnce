import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-notification-dialog',
  templateUrl: './notification-dialog.component.html',
  styleUrls: ['./notification-dialog.component.css']
})
export class NotificationDialogComponent {
  description!: string;
  message!:String ;


  constructor(@Inject(MAT_DIALOG_DATA) public data: any){
    this.description = data.description;
    this.message = data.message;
    
  }
}
  