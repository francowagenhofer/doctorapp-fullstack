import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ListadoEspecialidad } from '../especialidad/pages/listado-especialidad/listado-especialidad';
import { ListadoMedicoComponent } from '../medico/pages/listado-medico/listado-medico.component';
import { authGuard } from '../_guards/auth-guard';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    runGuardsAndResolvers: 'always', // Para que el guard se ejecute en cada navegación
    canActivate: [authGuard], // Protección de rutas con el guard de autenticación
    children: [
      { path: 'dashboard', component: DashboardComponent, pathMatch: 'full' },
      { path: 'especialidades', component: ListadoEspecialidad, pathMatch: 'full' },
      { path: 'medicos', component: ListadoMedicoComponent, pathMatch: 'full' },
      { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
    ]    
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }