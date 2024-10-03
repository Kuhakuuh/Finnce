import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TopNavBarComponent } from './components/top-nav-bar/top-nav-bar.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatSidenavModule} from '@angular/material/sidenav';
import { HomePageComponent } from './components/home-page/home-page.component';
import {MatListModule} from '@angular/material/list';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatCardModule} from '@angular/material/card';
import {MatMenuModule} from '@angular/material/menu';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { ContaComponent } from './components/conta/conta.component';
import {MatButtonModule} from '@angular/material/button';
import {MatExpansionModule} from '@angular/material/expansion';
import { DespesasComponent } from './components/despesas/despesas.component';
import {MatInputModule} from '@angular/material/input';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatTabsModule} from '@angular/material/tabs';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatSelectModule} from '@angular/material/select';
import {LayoutModule} from '@angular/cdk/layout';
import { WikiComponent } from './components/wiki/wiki.component';
import { ReceitasComponent } from './components/receitas/receitas.component';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegistoContaComponent } from './components/registo-conta/registo-conta.component';
import { MatNativeDateModule } from '@angular/material/core';
import { EntidadeComponent } from './components/entidade/entidade.component';
import { MatBadgeModule } from '@angular/material/badge';
import { PerfilComponent } from './components/perfil/perfil.component';
import { MatDialogModule } from '@angular/material/dialog';
import { NotificationComponent } from './components/notification/notification.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TransacoesComponent } from './components/transacoes/transacoes.component';
import {MatTableModule} from '@angular/material/table';
import { DashboardsComponent } from './components/dashboards/dashboards.component';
import { JwtInterceptorService } from './services/Guards/jwt-interceptor.service';
import { ToastrModule } from 'ngx-toastr';
import { NotificationPopUpComponent } from './components/notificationpopup/notificationpopup.component';
import { BarChartComponent } from './components/dashboards/bar-chart/bar-chart.component';
import { PieChartComponent } from './components/dashboards/pie-chart/pie-chart.component';
import { LineChartsComponent } from './components/dashboards/line-charts/line-charts.component';
import { SuccessMessageComponent } from './components/success-message/success-message.component';
import { NotificationDialogComponent } from './components/notification-dialog/notification-dialog.component';
import {NgToastModule} from 'ng-angular-popup';

@NgModule({
  declarations: [
    AppComponent,
    TopNavBarComponent,
    DespesasComponent,
    HomePageComponent,
    ContaComponent,
    WikiComponent,
    ReceitasComponent,
    LoginComponent,
    RegistoContaComponent,
    EntidadeComponent,
    PerfilComponent,
    NotificationComponent,
    DashboardsComponent,
    TransacoesComponent,
    BarChartComponent,
    NotificationPopUpComponent,
    PieChartComponent,
    LineChartsComponent,
    SuccessMessageComponent,
    NotificationDialogComponent
    

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatToolbarModule,
    MatIconModule,
    MatDividerModule,
    MatButtonToggleModule,
    MatSidenavModule,
    MatListModule,
    MatCheckboxModule,
    MatCardModule,
    MatMenuModule,
    BrowserAnimationsModule,
    MatProgressBarModule,
    MatButtonModule,
    MatGridListModule,
    MatExpansionModule,
    MatTabsModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatSelectModule,
    LayoutModule,
    MatSlideToggleModule,
    MatInputModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    FormsModule,
    MatBadgeModule,
    MatDialogModule,
    HttpClientModule,
    MatTableModule,
    NgToastModule 
    
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptorService,
      multi: true
    }
  ],
  
  bootstrap: [AppComponent]
})
export class AppModule { }
