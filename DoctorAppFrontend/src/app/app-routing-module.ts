import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './usuario/login/login.component';
import { LayoutComponent } from './compartido/layout/layout.component';

const routes: Routes = [
  {
    path: '', component: LoginComponent, pathMatch: 'full' // ruta principal redirige al login
  },
  {
    path: 'login', component: LoginComponent, pathMatch: 'full' // ruta para el login
  },
  {
    path: 'layout', 
    loadChildren: () => import('./compartido/compartido-module').then(m => m.CompartidoModule) // ruta para el layout compartido
  },
  {
    path: '**', redirectTo: '', pathMatch: 'full' // ruta comodin, redirige a la ruta principal
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

// enrutamiento principal de la aplicacion
// aqui se definen las rutas principales de la aplicacion
// las rutas hijas se definen en los modulos correspondientes
// sirve para cargar los componentes principales de la aplicacion