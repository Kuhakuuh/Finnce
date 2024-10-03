import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { AuthStorageService } from 'src/app/services/Guards/auth-storage.service';
import { AuthService } from 'src/app/services/Guards/auth.service';
import { SignalrService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-top-nav-bar',
  templateUrl: './top-nav-bar.component.html',
  styleUrls: ['./top-nav-bar.component.css']
})
export class TopNavBarComponent {
  public notificationCardOpen = false;
  isAuthenticated: boolean | undefined;
  constructor(private authstorage : AuthStorageService,private authService : AuthService,private router: Router,private singlarR:SignalrService) {

      this.isAuthenticated=  this.authstorage.isLoggedIn();
      this.singlarR.startConnection();
      this.singlarR.addTransactionListner();
    }

  
  toggleNotificationCard() {
    this.notificationCardOpen = !this.notificationCardOpen;
  }


   logout() {
      
    window.sessionStorage.clear();
     window.location.reload();
      this.router.navigate(['Login']);
         
  }


  @HostListener('document:click', ['$event'])
  handleClickOutside(event: Event) {
    const target = event.target as HTMLElement;

    if (
      this.notificationCardOpen &&
      !target.closest('.notification-card') &&
      !target.closest('.mat-button-toggle')
    ) {
      this.notificationCardOpen = false;
    }
  }
}
