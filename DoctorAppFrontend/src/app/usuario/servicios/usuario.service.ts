import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../Environments/environments';
import { Login } from '../interfaces/login';
import { Observable } from 'rxjs';
import { Sesion } from '../interfaces/sesion';


// Servicio de usuario para manejar todo lo relacionado con el usuario
@Injectable({
  providedIn: 'root',
})
export class UsuarioService {
  
  baseUrl: string = environment.apiUrl + 'usuario/';

  constructor(private http: HttpClient) {}

  iniciarSesion(request: Login): Observable<Sesion>{
    return this.http.post<Sesion>(this.baseUrl + 'login', request);
  }

}

