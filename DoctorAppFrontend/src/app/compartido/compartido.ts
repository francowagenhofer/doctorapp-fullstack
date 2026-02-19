import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Sesion } from '../usuario/interfaces/sesion';

@Injectable({
  providedIn: 'root',
})

// clase servicio para funcionalidades compartidas dentro de la app.
// metodos reutilizables en varios componentes  
export class Compartido { 
   
  //inyeccion de dependencias

  // servicio de barra de notificaciones
  constructor(private _snackBar: MatSnackBar) { }

  // metodo para mostrar una alerta tipo snackbar
  mostrarAlerta(mensaje : string, tipo: string) { 
    this._snackBar.open(mensaje, tipo, {
      horizontalPosition: 'end',  
      verticalPosition: 'top',
      duration: 3000
    });
  }

  // metodo para guardar la sesion del usuario en el almacenamiento local
  guardarSesion(sesion: Sesion) {
    localStorage.setItem('usuarioSesion', JSON.stringify(sesion.username)); // Guardar como cadena JSON
  }

  // metodo para obtener la sesion del usuario desde el almacenamiento local
  obetenerSesion() {
    const sesionString = localStorage.getItem('usuarioSesion'); // Obtener la cadena JSON
    const usuarioSesion = JSON.parse(sesionString!); // Convertir de cadena JSON a objeto
    
    return usuarioSesion;
  }

  // metodo para eliminar la sesion del usuario desde el almacenamiento local
  eliminarSesion() {
    localStorage.removeItem('usuarioSesion'); // Eliminar la sesion del almacenamiento local
  }

}
