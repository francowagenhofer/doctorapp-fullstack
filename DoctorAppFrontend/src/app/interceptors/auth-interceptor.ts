// import { Injectable } from '@angular/core';
// import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
// import { Observable } from 'rxjs';

// @Injectable()
// export class AuthInterceptor implements HttpInterceptor {
 
//   intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
//     const sesionString = localStorage.getItem('usuarioSesion');

//     if (sesionString) {
//       const sesion = JSON.parse(sesionString);
//       const token = sesion?.token;

//       if (token) {
//         const clonedRequest = req.clone({
//           setHeaders: {
//             Authorization: `Bearer ${token}`
//           }
//         });

//         return next.handle(clonedRequest);
//       }
//     }

//     return next.handle(req);
//   }
// }


import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private cookieService: CookieService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const authRequest = request.clone({
      setHeaders: {
        'Authorization': this.cookieService.get('Authorization')
      }
    })

    return next.handle(authRequest);
  }
}
