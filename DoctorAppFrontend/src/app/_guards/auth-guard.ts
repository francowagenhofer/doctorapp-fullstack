import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Compartido } from '../compartido/compartido';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';

// Guard de autenticación para proteger rutas
export const authGuard: CanActivateFn = (route, state) => {

  const compartidoServicio = inject(Compartido);
  const router = inject(Router);
  const cookieService = inject(CookieService);
  const usuario = compartidoServicio.obetenerSesion();
  
  let token = cookieService.get('Authorization');

  if (token && usuario) {
    token = token.replace('Bearer ', '');
    const decodedToken: any = jwtDecode(token);
    const fechaExpiracion = new Date(decodedToken.exp * 1000);
    const fechaActual = new Date();

    if (fechaExpiracion < fechaActual) {
      router.navigate(['/login']);
      return false;
    }
    return true;
  }
  else {
    router.navigate(['/login']);
    return false;
  }
};
