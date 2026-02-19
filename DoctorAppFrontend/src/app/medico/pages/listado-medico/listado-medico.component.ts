import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Medico } from '../../interfaces/medico';
import { MedicoService } from '../../servicios/medico.service';
import { Compartido } from '../../../compartido/compartido';
import Swal from 'sweetalert2';
import { MatDialog } from '@angular/material/dialog';
import { ModalMedicoComponent } from '../../modales/modal-medico/modal-medico.component';

@Component({
  selector: 'app-listado-medico',
  standalone: false,
  templateUrl: './listado-medico.component.html',
  styleUrls: ['./listado-medico.component.css'],
})
export class ListadoMedicoComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['nombres', 'apellidos', 'telefono', 'genero', 'nombreEspecialidad', 'estado', 'acciones'];

  dataInicial: Medico[] = [];

  dataSource = new MatTableDataSource(this.dataInicial);
  @ViewChild(MatPaginator) paginacionTabla!: MatPaginator;

  constructor(
    private _medicoService: MedicoService,
    private _compartidoServicio: Compartido,
    private dialog: MatDialog
  ) { }

  // Método para obtener los médicos
  obtenerMedicos() {
    this._medicoService.lista().subscribe({
      next: (data) => {
        if (data.esExitoso) {
          this.dataSource = new MatTableDataSource(data.resultado);
          this.dataSource.paginator = this.paginacionTabla;
        }
        else {
          this._compartidoServicio.mostrarAlerta("No se encontraron datos", "Advertencia!");
        }
      },
      error: (e) => {
        this._compartidoServicio.mostrarAlerta("Error al cargar médicos: " + e.error.message, "Error!");
      }
    });
  }

  nuevoMedico() {
    this.dialog
        .open(ModalMedicoComponent, { disableClose: true, width: '600px'})
        .afterClosed()
        .subscribe((resultado) => {
          if (resultado === 'true') this.obtenerMedicos();
        });
  }

  editarMedico(medico: Medico) {
    this.dialog
        .open(ModalMedicoComponent, { disableClose: true, width: '600px', data: medico })
        .afterClosed()
        .subscribe((resultado) => {
          if (resultado === 'true') this.obtenerMedicos();
        });
  }

  removerMedico(medico: Medico) {
    Swal.fire({
      title: '¿Desea eliminar el médico?',
      text: `${medico.nombres} ${medico.apellidos}`,  
      icon: 'warning',
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Sí, eliminar',
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'No, cancelar'
    }).then((resultado) => {
      if (resultado.isConfirmed) {
        this._medicoService.eliminar(medico.id).subscribe({
          next: (data) => {
            if (data.esExitoso) {
              this._compartidoServicio.mostrarAlerta("Médico eliminado con éxito", "Éxito");
              this.obtenerMedicos();
            } else {
              this._compartidoServicio.mostrarAlerta("No se pudo eliminar el médico", "Error");
            }
          },
          error: (e) => {
            this._compartidoServicio.mostrarAlerta("Error al eliminar médico: " + e.message, "Error");
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

  ngOnInit(): void {
    this.obtenerMedicos();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginacionTabla;
  }
}
