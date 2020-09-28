import { Component, Input, OnInit } from "@angular/core";
import { ChartDataSets, ChartOptions } from "chart.js";
import { Color, Label } from "ng2-charts";
import { LongWeatherForecastModel } from "../shared/models/longWeatherForecastModel";

@Component({
  selector: "app-line-chart",
  templateUrl: "./line-chart.component.html",
  styleUrls: ["./line-chart.component.css"],
})
export class LineChartComponent implements OnInit {
  constructor() {}

  @Input() dataSets: LongWeatherForecastModel;

  lineChartData: ChartDataSets[] = [];

  lineChartLabels: Label[] = [];

  lineChartOptions: ChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
  };

  lineChartColors: Color[] = [
    {
      borderColor: "black",
      backgroundColor: "#FF0004",
    },
  ];

  lineChartLegend = true;
  lineChartPlugins = [];
  lineChartType = "line";

  ngOnInit(): void {
    this.lineChartData.push({
      data: this.dataSets.list.map((x) => x.main.tempC),
      label: "Temperature",
    });

    this.lineChartLabels.push(...this.dataSets.list.map((x) => x.dayName));
  }
}
