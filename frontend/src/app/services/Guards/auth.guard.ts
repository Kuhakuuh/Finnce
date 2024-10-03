import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';


export const authGuard: CanActivateFn = (route, state) => {
  const token = window.sessionStorage.getItem('token');

  const router = inject(Router);

  if (token && typeof token === 'string' && token.trim() !== '') {
    

    if (state.url.includes('Login')) {
      router.navigate(['dashboards']);
    }

    return true;
  } else {
    router.navigate(['Login']);
    return false;
  }
};

