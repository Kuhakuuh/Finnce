import { Component, OnInit } from '@angular/core';
import { ChartService } from 'src/app/services/Chart/chart.service';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit {
  public chart: any;
  public categoryData: any;

  constructor(private chartService: ChartService) {}

  ngOnInit(): void {
    this.getDataFromService();
  }

  getDataFromService(): void {
    this.chartService.getDataForMonthInYear().subscribe(
      (data: any) => {
        if (data && data.result && data.result.CategoryData) {
          this.categoryData = data.result.CategoryData;
          this.createChart();
        }
      },
      (error) => {
        console.error('Erro ao obter dados do serviço:', error);
      }
    );
  }

  createChart(): void {
    // converte o dicionario para array
    const labels = Object.keys(this.categoryData);
    const values: number[] = Object.values(this.categoryData);

    // remove o primeiro elemento do array
    labels.shift();
    values.shift();

    this.chart = new Chart("MyPie", {
      type: 'pie',
      data: {
        labels: labels,
        datasets: [
          {
            data: values,
            backgroundColor: [
              'rgb(255, 99, 132)',
              'rgb(54, 162, 235)',
              'rgb(255, 205, 86)',
              'rgb(100,149,237)',
              'rgb(46,139,87)',
              'rgb(160,82,45)',
              'rgb(70,130,180)',
              'rgb(255,140,0)',
              'rgb(128,0,128)',
              'rgb(0,128,0)',
              'rgb(0,0,128)',
              'rgb(128,128,128)',
            ],
          },
        ],
      },
      options: {
        aspectRatio: 2.5
      }
    });
  }
}
