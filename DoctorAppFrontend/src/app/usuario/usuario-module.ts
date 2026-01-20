import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';
import { CompartidoModule } from '../compartido/compartido-module';
import {UsuarioService} from './servicios/usuario.service';
import { LoginComponent } from './login/login.component';
import { MaterialModule } from '../material/material-module';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    NgOptimizedImage,
    CompartidoModule,
    MaterialModule
  ],
  exports:[
  LoginComponent
  ],

  providers: [
    UsuarioService
  ]
})
export class UsuarioModule { }
