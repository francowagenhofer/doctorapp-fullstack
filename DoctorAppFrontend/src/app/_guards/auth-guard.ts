import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Compartido } from '../compartido/compartido';


// Guard de autenticación para proteger rutas
export const authGuard: CanActivateFn = (route, state) => {

  const compartidoServicio = inject(Compartido);
  const router = inject(Router);
  
  const usuarioToken = compartidoServicio.obetenerSesion();
  if (usuarioToken != null) {
    return true;
  }
  else {
    router.navigate(['/login']);
    return false;
  }
};
