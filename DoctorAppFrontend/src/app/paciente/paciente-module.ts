import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompartidoModule } from '../compartido/compartido-module';
import { MaterialModule } from '../material/material-module';
import { ListadoPacienteComponent } from './pages/listado-paciente/listado-paciente.component';
import { PacienteService } from './servicios/paciente';

@NgModule({
  declarations: [
    ListadoPacienteComponent
  ],
  imports: [
    CommonModule,
    CompartidoModule, 
    MaterialModule
  ]
  ,
  providers: [
    PacienteService
  ]
})
export class PacienteModule { }
