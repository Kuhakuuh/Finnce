import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { ContaComponent } from './components/conta/conta.component';
import { DespesasComponent } from './components/despesas/despesas.component';
import { WikiComponent } from './components/wiki/wiki.component';
import { ReceitasComponent } from './components/receitas/receitas.component';
import { LoginComponent } from './components/login/login.component';
import { RegistoContaComponent } from './components/registo-conta/registo-conta.component';
import { EntidadeComponent } from './components/entidade/entidade.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { NotificationComponent } from './components/notification/notification.component';
import { TransacoesComponent } from './components/transacoes/transacoes.component'; 
import { DashboardsComponent } from './components/dashboards/dashboards.component';
import { authGuard } from './services/Guards/auth.guard';

const routes: Routes = [
    { path: '', component: DashboardsComponent, canActivate:[authGuard] },
    { path: 'contas', component: ContaComponent, canActivate:[authGuard] },
    { path: 'despesas', component: DespesasComponent, canActivate:[authGuard] },
    { path: 'wiki', component: WikiComponent, canActivate:[authGuard] },
    { path: 'receitas', component: ReceitasComponent, canActivate:[authGuard] },
    { path: 'Login', component: LoginComponent},
    { path: 'registo-conta', component: RegistoContaComponent},
    { path: 'entidade', component: EntidadeComponent, canActivate:[authGuard] },
    {path: 'perfil', component: PerfilComponent, canActivate:[authGuard]},
    {path: 'notification', component: NotificationComponent, canActivate:[authGuard]},
    {path: 'transacoes',component: TransacoesComponent, canActivate:[authGuard]},
    {path: 'dashboards', component: DashboardsComponent,canActivate:[authGuard]},
    {path: 'Home', component: HomePageComponent, canActivate:[authGuard]}
    
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
