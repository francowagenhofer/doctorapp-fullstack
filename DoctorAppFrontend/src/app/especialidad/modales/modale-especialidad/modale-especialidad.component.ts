import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Especialidad } from '../../interfaces/especialidad';
import { EspecialidadService } from '../../servicios/especialidad.service';
import { Compartido } from '../../../compartido/compartido';
import { E } from '@angular/cdk/keycodes';

@Component({
  selector: 'app-modale-especialidad',
  standalone: false,
  templateUrl: './modale-especialidad.component.html',
  styleUrl: './modale-especialidad.component.css',
})
export class ModaleEspecialidadComponent implements OnInit {

  formEspecialidad: FormGroup;
  titulo: string = "Agregar";
  nombreBoton: string = "Guardar";
  datosEspecialidad: Especialidad = inject(MAT_DIALOG_DATA);

  constructor(private modal: MatDialogRef<ModaleEspecialidadComponent>,
    private fb: FormBuilder,
    private _especialidadServicio: EspecialidadService,
    private _compartidoServicio: Compartido) {

    this.formEspecialidad = this.fb.group({
      nombreEspecialidad: ['', Validators.required],
      descripcion: ['', Validators.required],
      estado: ['1', Validators.required]
    })

    if (this.datosEspecialidad != null) {
      this.titulo = "Editar";
      this.nombreBoton = "Actualizar";
    }

  }

  ngOnInit(): void {
    if (this.datosEspecialidad != null) {
      this.formEspecialidad.patchValue({
        nombreEspecialidad: this.datosEspecialidad.nombreEspecialidad,
        descripcion: this.datosEspecialidad.descripcion,
        estado: this.datosEspecialidad.estado.toString()
      });
    }

  }

  crearModificarEspecialidad() {
    const especialidad: Especialidad = {
      id: this.datosEspecialidad != null ? this.datosEspecialidad.id : 0,
      nombreEspecialidad: this.formEspecialidad.value.nombreEspecialidad,
      descripcion: this.formEspecialidad.value.descripcion,
      estado: parseInt(this.formEspecialidad.value.estado)
    };

    if (this.datosEspecialidad == null) {
      this._especialidadServicio.crear(especialidad).subscribe({
        next: (data) => {
          if (data.esExitoso) {
            this._compartidoServicio.mostrarAlerta("Especialidad creada con éxito", "Éxito");
            this.modal.close("true");
          }
          else
            this._compartidoServicio.mostrarAlerta("No se pudo crear la especialidad", "Error");
        },
        error: (e) => {
          this._compartidoServicio.mostrarAlerta("Error al crear especialidad: " + e.message, "Error");
        }
      });
    }
    else {
      this._especialidadServicio.editar(especialidad).subscribe({
        next: (data) => {
          if (data.esExitoso) {
            this._compartidoServicio.mostrarAlerta("Especialidad actualizada con éxito", "Éxito");
            this.modal.close("true");
          }
          else
            this._compartidoServicio.mostrarAlerta("No se pudo actualizar la especialidad", "Error");
        },
        error: (e) => {
          this._compartidoServicio.mostrarAlerta("Error al actualizar especialidad: " + e.message, "Error");
        }
      });
    }
  }

}