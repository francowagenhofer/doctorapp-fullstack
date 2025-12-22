import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrls: ['./app.css'] // plural
})
export class App implements OnInit {
  title: string = 'DoctorApp';
  usuarios: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<any>('http://localhost:5069/api/usuario').subscribe({
      next: (response: any) => (this.usuarios = response),
      error: (err: any) => console.error('Error al obtener los usuarios', err),
      complete: () => console.log('Solicitud completada')
    });
  }
}