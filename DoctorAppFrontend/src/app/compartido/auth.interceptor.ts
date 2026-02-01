import { HttpInterceptorFn } from '@angular/common/http';

// Interceptor HTTP que agrega automáticamente el token JWT a todas las peticiones HTTP
// Los interceptores son funciones que se ejecutan antes de cada petición HTTP
// Esto evita tener que agregar manualmente el header de autorización en cada servicio
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  // Obtener la sesión del usuario desde el localStorage
  const sesionString = localStorage.getItem('usuarioSesion');
  
  // Si existe una sesión guardada
  if (sesionString) {
    const sesion = JSON.parse(sesionString);
    const token = sesion?.token;
    
    // Si el token existe, clonar la petición HTTP y agregar el header de autorización
    if (token) {
      const clonedRequest = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}` // Formato estándar para tokens JWT
        }
      });
      // Continuar con la petición modificada
      return next(clonedRequest);
    }
  }
  
  // Si no hay token, continuar con la petición original sin modificar
  return next(req);
};
