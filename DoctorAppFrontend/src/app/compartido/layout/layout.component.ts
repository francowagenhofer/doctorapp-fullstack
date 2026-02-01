import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Compartido as CompartidoService } from '../compartido';

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
  
  constructor(private router: Router, private compartidoService: CompartidoService)
  {

  }

  // Metodo que se ejecuta al inicializar el componente. 
  // Sirve para obtener el nombre del usuario desde la sesion guardada y mostrarlo en el layout
  ngOnInit(): void {
    const usuarioToken = this.compartidoService.obetenerSesion();  
    if (usuarioToken != null) {
      this.username = usuarioToken.username;
    }
  }

  cerrarSesion() {
    this.compartidoService.eliminarSesion();
    this.router.navigate(['/login']);
  }


}
