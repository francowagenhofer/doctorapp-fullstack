import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Especialidad } from '../../interfaces/especialidad';
import { EspecialidadService } from '../../servicios/especialidad.service';
import { Compartido } from '../../../compartido/compartido';
import { MatDialog } from '@angular/material/dialog';
import { ModaleEspecialidadComponent } from '../../modales/modale-especialidad/modale-especialidad.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-listado-especialidad',
  standalone: false,
  templateUrl: './listado-especialidad.html',
  styleUrl: './listado-especialidad.css',
})
export class ListadoEspecialidad implements OnInit, AfterViewInit {
  
  displayedColumns: string[] = ['nombreEspecialidad', 'descripcion', 'estado', 'acciones'];

  dataInicial: Especialidad[] = [];

  dataSource = new MatTableDataSource(this.dataInicial);
  @ViewChild(MatPaginator) paginacionTabla!: MatPaginator;

  constructor(private _especialidadServicio: EspecialidadService,
              private _compartidoServicio: Compartido,
              private dialog: MatDialog) {}

  // Método para abrir el modal de nueva especialidad
  nuevaEspecialidad() {
    this.dialog.open(ModaleEspecialidadComponent, {
      disableClose: true,
      width: '400px'
    })
    .afterClosed()
    .subscribe((resultado) => {
      if (resultado === "true") this.obtenerEspecialidades();
    });
  }
  
  editarEspecialidad(especialidad: Especialidad) {
    this.dialog
    .open(ModaleEspecialidadComponent, {
      disableClose: true, 
      width: '400px',
      data: especialidad
    })
    .afterClosed()
    .subscribe((resultado) => {
      if (resultado === "true") this.obtenerEspecialidades();
    }); 
  }

  // Método para obtener las especialidades
  obtenerEspecialidades() {
    this._especialidadServicio.lista().subscribe({
      next: (data) => {
        if(data.esExitoso)
        {
          this.dataSource = new MatTableDataSource(data.resultado);
          this.dataSource.paginator = this.paginacionTabla;
        }
        else 
        {
          this._compartidoServicio.mostrarAlerta("No se encontraron datos", "Advertencia!");
        }
      },
      error: (e) => {
        // Agregar log en consola para debugging
        console.error('Error al obtener especialidades:', e);
        // Mostrar mensaje de error al usuario mediante snackbar
        // Esto es CRÍTICO: sin este manejo, los errores HTTP serían silenciosos
        this._compartidoServicio.mostrarAlerta("Error al cargar especialidades: " + e.message, "Error!");
      }
    });
  }

  eliminarEspecialidad(especialidad: Especialidad) {
   Swal.fire({
      title: '¿Desea eliminar la especialidad?',
      text: `${especialidad.nombreEspecialidad}`,  
      icon: 'warning',
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Sí, eliminar',
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'No, cancelar'
   }).then((resultado) => {
      if (resultado.isConfirmed) {
         this._especialidadServicio.eliminar(especialidad.id).subscribe({
            next: (data) => {
               if (data.esExitoso) {
                  this._compartidoServicio.mostrarAlerta("Especialidad eliminada con éxito", "Éxito");
                  this.obtenerEspecialidades();
               } else {
                  this._compartidoServicio.mostrarAlerta("No se pudo eliminar la especialidad", "Error");
               }
            },
            error: (e) => {
               this._compartidoServicio.mostrarAlerta("Error al eliminar especialidad: " + e.message, "Error");
            }
         });
      }
   });
  }

  aplicarFiltroListado(event: Event) {
    const filtroValor = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filtroValor.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


  // Ciclo de vida del componente
  ngOnInit(): void {
    this.obtenerEspecialidades();
  }

  // Después de que la vista ha sido inicializada completamente. Es un buen lugar para inicializar componentes dependientes de la vista.
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginacionTabla;
  }

}
