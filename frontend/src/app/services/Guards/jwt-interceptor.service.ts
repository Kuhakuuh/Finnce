import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthStorageService } from './auth-storage.service';

@Injectable({
  providedIn: 'root'
})


export class JwtInterceptorService implements HttpInterceptor  {
  

  constructor(private authStorageService :AuthStorageService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> 
  {

    
    const authResponse  = this.authStorageService.getToken();
    if (authResponse ) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${authResponse}`
        }
      });
    }
    return next.handle(request);
  }





}
