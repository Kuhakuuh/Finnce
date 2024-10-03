import { Component, OnInit } from '@angular/core';
import { ChartService } from 'src/app/services/Chart/chart.service';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent implements OnInit {
  public chart: any;
  expenseData: any;

  constructor(private chartService: ChartService) {}

  ngOnInit(): void {
    this.createChart();
  }
  createChart(): void {
    this.chartService.getDataForMonthInYear().subscribe(
      (data: any) => {
        if (data && data.result) {
          this.expenseData = data.result;
  
          const dateKeys = Object.keys(this.expenseData).map(key => new Date(key));
          dateKeys.sort((a, b) => a.getTime() - b.getTime());
  
          // Extrair os 12 rótulos para os meses do ano
          const sortedLabels = dateKeys
            .slice(0, 13)
            .map(date => {
              const month = date.toLocaleString('default', { month: 'long' });
              return `${month}`;
            });
  
          // Retira do dicionário as receitas e as despesas
          const values: [string, string][] = Object.entries(this.expenseData);
          const expenses: number[] = values.slice(0, 12).map(item => parseFloat(item[1][1].replace(',', '.')));
          const revenues: number[] = values.slice(12, 24).map(item => parseFloat(item[1][1].replace(',', '.')));
          sortedLabels.shift();
  
          this.chart = new Chart("MyChart", {
            type: 'bar',
            data: {
              labels: sortedLabels,
              datasets: [
                {
                  label: "Receitas",
                  data: revenues,
                  backgroundColor: 'green'
                },
                {
                  label: "Despesas",
                  data: expenses,
                  backgroundColor: 'red'
                },
              ]
            },
            options: {
              aspectRatio: 2.5
            }
          });
        }
      },
      (error) => {
        console.error('Erro ao obter contas:', error);
      }
    );
  }
}
