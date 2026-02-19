import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { UsuarioModule } from './usuario/usuario-module';
import { EspecialidadModule } from './especialidad/especialidad-module';
import { MedicoModule } from './medico/medico-module';
import { AuthInterceptor } from './interceptors/auth-interceptor';

@NgModule({
  declarations: [App],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    UsuarioModule,
    EspecialidadModule,
    MedicoModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [App]
})
export class AppModule {}