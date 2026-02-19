import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Especialidad } from '../../../especialidad/interfaces/especialidad';
import { Medico } from '../../interfaces/medico';
import { MedicoService } from '../../servicios/medico.service';
import { EspecialidadService } from '../../../especialidad/servicios/especialidad.service';
import { Compartido } from '../../../compartido/compartido';

@Component({
  selector: 'app-modal-medico',
  standalone: false,
  templateUrl: './modal-medico.component.html',
  styleUrls: ['./modal-medico.component.css'],
})
export class ModalMedicoComponent implements OnInit {

  formMedico: FormGroup;
  titulo: string = "Agregar";
  nombreBoton: string = "Guardar";
  listaEspecialidades: Especialidad[] = [];
  dataMedico: Medico = inject(MAT_DIALOG_DATA);

  constructor(
    private modal: MatDialogRef<ModalMedicoComponent>,
    private fb: FormBuilder,
    private _especialidadService: EspecialidadService,
    private _medicoService: MedicoService,
    private _compartidoServicio: Compartido
  ) {
    this.formMedico = this.fb.group({
      nombres: ['', Validators.required],
      apellidos: ['', Validators.required],
      direccion: ['', Validators.required],
      telefono: [''],
      genero: ['M', Validators.required],
      especialidadId: ['', Validators.required],
      estado: ['1', Validators.required]
    });
    
    if (this.dataMedico != null) {
      this.titulo = "Editar";
      this.nombreBoton = "Actualizar";
    }
  }

  ngOnInit(): void {
    this._especialidadService.listaActivos().subscribe({
      next: (data) => {
        if (data.esExitoso) {
          this.listaEspecialidades = data.resultado;
        }
      },
      error: (e) => {}
    });

    if (this.dataMedico != null) {
      this.formMedico.patchValue({
        nombres: this.dataMedico.nombres,
        apellidos: this.dataMedico.apellidos,
        direccion: this.dataMedico.direccion,
        telefono: this.dataMedico.telefono,
        genero: this.dataMedico.genero,
        especialidadId: this.dataMedico.especialidadId,
        estado: this.dataMedico.estado.toString(),
      });
    }
  }

  crearModificarMedico() {
    const medico: Medico = {
      id: this.dataMedico != null ? this.dataMedico.id : 0,
      nombres: this.formMedico.value.nombres,
      apellidos: this.formMedico.value.apellidos,
      direccion: this.formMedico.value.direccion,
      telefono: this.formMedico.value.telefono,
      genero: this.formMedico.value.genero,
      especialidadId: parseInt(this.formMedico.value.especialidadId),
      estado: parseInt(this.formMedico.value.estado),
      nombreEspecialidad: ''
    };
    
    if (this.dataMedico == null) {
      this._medicoService.crear(medico).subscribe({
        next: (data) => {
          if (data.esExitoso) {
            this._compartidoServicio.mostrarAlerta("Médico creado con éxito", "Éxito");
            this.modal.close("true");
          }
          else
            this._compartidoServicio.mostrarAlerta("No se pudo crear el médico", "Error");
        },
        error: (e) => {
          this._compartidoServicio.mostrarAlerta(e.error.message, "Error");
        }
      });
    }
    else {
      this._medicoService.editar(medico).subscribe({
        next: (data) => {
          if (data.esExitoso) {
            this._compartidoServicio.mostrarAlerta("Médico actualizado con éxito", "Éxito");
            this.modal.close("true");
          }
          else
            this._compartidoServicio.mostrarAlerta("No se pudo actualizar el médico", "Error");
        },
        error: (e) => {
          this._compartidoServicio.mostrarAlerta(e.error.message, "Error");
        }
      });
    }
  }
}
