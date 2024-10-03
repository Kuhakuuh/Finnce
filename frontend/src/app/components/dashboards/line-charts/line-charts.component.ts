import { Component, OnInit } from '@angular/core';
import * as Chart from 'chart.js';
import { ChartService } from 'src/app/services/Chart/chart.service';

@Component({
  selector: 'app-line-charts',
  templateUrl: './line-charts.component.html',
  styleUrls: ['./line-charts.component.css'],
})
export class LineChartsComponent implements OnInit {
  public chart: any;
  public expenseData: any;

  constructor(private chartService: ChartService) {}

  ngOnInit(): void {
    this.createChart();
  }

  createChart(): void {
    this.chartService.getDataForMonthInYear().subscribe(
      (data: any) => {
        if (data && data.result) {
          this.expenseData = data.result;
  
          // Extrair as datas e ordenar
          const dateKeys = Object.keys(this.expenseData).map(key => new Date(key));
          dateKeys.sort((a, b) => a.getTime() - b.getTime());
  
          // Extrair os 13 rótulos para os meses do ano
          const sortedLabels = dateKeys
            .slice(0, 13)
            .map((date) => date.toLocaleString('default', { month: 'long' }))
            .filter((value, index, self) => self.indexOf(value) === index); // Remove duplicatas
  
          // Retira do dicionário as receitas e as despesas
          const values: [string, string][] = Object.entries(this.expenseData);
          const expenses: number[] = values.slice(0, 12).map(item => parseFloat(item[1][1].replace(',', '.')));
          const revenues: number[] = values.slice(12, 24).map(item => parseFloat(item[1][1].replace(',', '.')));
  
          // Criar o gráfico
          this.chart = new Chart('MyLineChart', {
            type: 'line',
            data: {
              labels: sortedLabels,
              datasets: [
                {
                  label: 'Receitas',
                  data: revenues,
                  fill: false,
                  borderColor: 'green',
                },
                {
                  label: 'Despesas',
                  data: expenses,
                  fill: false,
                  borderColor: 'red',
                },
              ],
            },
            options: {
              aspectRatio: 2.5,
            },
          });
        }
      },
      (error) => {
        console.error('Erro ao obter dados do serviço de gráfico:', error);
      }
    );
  }
  
  
  

  getKeyForLabel(label: string): string {
    const monthNames: { [key: string]: string } = {
      'Janeiro': '1',
      'Fevereiro': '2',
      'Março': '3',
      'Abril': '4',
      'Maio': '5',
      'Junho': '6',
      'Julho': '7',
      'Agosto': '8',
      'Setembro': '9',
      'Outubro': '10',
      'Novembro': '11',
      'Dezembro': '12',
    };

    return monthNames[label];
  }
}
