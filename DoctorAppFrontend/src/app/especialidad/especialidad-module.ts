import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompartidoModule } from '../compartido/compartido-module';
import { MaterialModule } from '../material/material-module';
import { EspecialidadService } from './servicios/especialidad.service';
import { ListadoEspecialidad } from './pages/listado-especialidad/listado-especialidad';
import { ModaleEspecialidadComponent } from './modales/modale-especialidad/modale-especialidad.component';

// Módulo para funcionalidades relacionadas con especialidades

@NgModule({
  declarations: [
    ListadoEspecialidad,
    ModaleEspecialidadComponent
  ],
  imports: [
    CommonModule, // Módulo común de Angular
    CompartidoModule, // Módulo compartido para componentes reutilizables
    MaterialModule  // Módulo de Angular Material para componentes UI
  ], 
  providers: [
    EspecialidadService
  ]
})
export class EspecialidadModule { }

