import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../Environments/environments';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../interfaces/api-response';
import { Especialidad } from '../interfaces/especialidad';
import { CookieService } from 'ngx-cookie-service';

// Servicio para gestionar las operaciones relacionadas con especialidades médicas
// Proporciona métodos para listar, crear, editar y eliminar especialidades
// Esta conectadas a la API RESTful y vinculadas a los endpoints correspondientes
// Utiliza HttpClient para realizar solicitudes HTTP
// Los componentes pueden inyectar este servicio para interactuar con las especialidades

@Injectable({
  providedIn: 'root',
})
export class EspecialidadService {
  // URL del endpoint de especialidades en el backend
  // IMPORTANTE: Cambié 'especialidades' (plural) a 'especialidad' (singular)
  // para que coincida con la ruta del backend. El error 404 indicaba que el endpoint
  // en el servidor usa /api/especialidad en lugar de /api/especialidades
  baseUrl: string = environment.apiUrl + 'especialidad/';

  constructor(private http: HttpClient, private cookieService: CookieService) {}
  
  lista() : Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${this.baseUrl}`);
  }

  listaActivos() : Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${this.baseUrl}listadoActivos`);
  }

  crear(request: Especialidad) : Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${this.baseUrl}`, request);
  }

  editar(request: Especialidad) : Observable<ApiResponse> {
    return this.http.put<ApiResponse>(`${this.baseUrl}`, request);
  }
   
  eliminar(id: number) : Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(`${this.baseUrl}${id}`);
  }  
}
