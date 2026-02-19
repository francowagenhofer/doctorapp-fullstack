import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Compartido as CompartidoService } from '../compartido';
import { CookieService } from 'ngx-cookie-service'

@Component({
  selector: 'app-layout',
  standalone: false,
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css',
})

// Componente de layout principal de la aplicacion.
// Contiene la estructura basica de la interfaz de usuario, como la barra de navegacion y el area de contenido.
export class LayoutComponent implements OnInit {

  username: string = '';
  
  constructor(private router: Router, private compartidoService: CompartidoService, private cookieService: CookieService)
  {

  }

  // Metodo que se ejecuta al inicializar el componente. 
  // Sirve para obtener el nombre del usuario desde la sesion guardada y mostrarlo en el layout
  ngOnInit(): void {
    const usuarioSesion = this.compartidoService.obetenerSesion();  
    if (usuarioSesion != null) {
      this.username = usuarioSesion;
    }
  }

  cerrarSesion() {
    this.compartidoService.eliminarSesion();

    this.cookieService.delete('Authorization','/');

    this.router.navigate(['/login']);
  }


}
