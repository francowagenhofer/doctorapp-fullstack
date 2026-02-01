import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { UsuarioModule } from './usuario/usuario-module';
import { EspecialidadModule } from './especialidad/especialidad-module';
import { MedicoModule } from './medico/medico-module';
import { authInterceptor } from './compartido/auth.interceptor';

@NgModule({
  declarations: [
    App
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    UsuarioModule,
    EspecialidadModule,
    MedicoModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptors([authInterceptor]))
  ],
  bootstrap: [App]
})
export class AppModule { }
