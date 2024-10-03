import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../../services/accounts.service';
import { ChartService } from 'src/app/services/Chart/chart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboards',
  templateUrl: './dashboards.component.html',
  styleUrls: ['./dashboards.component.css'],
})
export class DashboardsComponent implements OnInit {
  receitasNoAno: number = 0;
  receitasValue: number = 0;
  despesasValue: number = 0;
  despesasNoAno: number =0;

  constructor(
    private chartService: ChartService,
  ) {}

  ngOnInit() {
    this.getDataFromServices();
  }

  getDataFromServices(): void {
    this.chartService.getDataForMonthInYear().subscribe(
      (data: any) => {
        if (data && data.result) {
          const result = data.result;
          this.despesasNoAno = result.ExpenseInYear;
          this.receitasNoAno = result.totalRevenueYear;
          this.receitasValue = result.totalRevenueInActualyMonth;
          this.despesasValue = result.totalExpenseInActualyMonth;
        }
      },
      (error) => {
        console.error('Erro ao obter dados do serviço de gráfico:', error);
      }
    );
  }
}
