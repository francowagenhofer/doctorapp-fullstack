import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { ReactiveFormsModule, FormsModule} from '@angular/forms'; // sirve para formularios reactivos y plantillas
import { HttpClientModule } from '@angular/common/http';
import { LayoutComponent } from './layout/layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MaterialModule } from '../material/material-module';


@NgModule({
  declarations: [ 
    LayoutComponent, DashboardComponent
  ], 
  imports: [
    CommonModule,
    MaterialModule
  ],
  exports: [
    ReactiveFormsModule,
    FormsModule, HttpClientModule,
    LayoutComponent, DashboardComponent
  ]
})
export class CompartidoModule { }
