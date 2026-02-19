import { Injectable } from '@angular/core';
import { environment } from '../../../Environments/environments';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '../../interfaces/api-response';
import { Observable } from 'rxjs';
import { Paciente } from '../interfaces/paciente';

@Injectable({
  providedIn: 'root',
})
export class PacienteService {

  baseUrl: string = environment.apiUrl + 'historiaclinica/';  

  constructor(private http: HttpClient ) { }

  lista(): Observable<ApiResponse>{
    return this.http.get<ApiResponse>(`${this.baseUrl}`);
  }

  crear(request: Paciente): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${this.baseUrl}`, request);
  }

}
