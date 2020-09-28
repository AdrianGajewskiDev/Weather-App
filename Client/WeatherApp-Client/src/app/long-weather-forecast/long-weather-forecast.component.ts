import { Component, OnInit } from "@angular/core";
import { slideAnimation } from "../shared/animations/animations";
import { ActivatedRoute } from "@angular/router";
import { WeatherService } from "../shared/services/weather.service";
import { LongWeatherForecastModel } from "../shared/models/longWeatherForecastModel";
import { LongWeatherForecastListItemModel } from "../shared/models/longWeatherForecastListItemModel";
import { determineCurrentWeatherImage } from "../shared/weatherConditions/weatherDescriptions";

interface TableRow {
  day: string;
  tempC: number;
  pressure: number;
}

interface WeatherDetailsRows {
  temperatureC: number;
  temperatureF: number;
  pressure: number;
  wind: number;
}

@Component({
  selector: "app-long-weather-forecast",
  templateUrl: "./long-weather-forecast.component.html",
  styleUrls: ["./long-weather-forecast.component.css"],
})
export class LongWeatherForecastComponent implements OnInit {
  cityName: string = "";
  state: "slideIn" | "slideOut" = "slideIn";
  longWeatherModel: LongWeatherForecastModel;
  tableData: TableRow[] = [];
  displayedColumns: string[] = ["Day", "TempC", "Pressure"];
  dataSource;
  dataSourceInWeatherDetails;
  loadingData: boolean = true;
  imageSrc: string = "";
  currentWeatherDetails: LongWeatherForecastListItemModel;
  constructor(
    private routes: ActivatedRoute,
    private weatherService: WeatherService
  ) {}

  ngOnInit(): void {
    this.routes.params.subscribe((param) => {
      this.cityName = param["city"];
    });

    this.weatherService
      .get6DaysWeatherForecast(this.cityName)
      .subscribe((res) => {
        this.longWeatherModel = res.responseBody;
        this.longWeatherModel.list.forEach((element) => {
          this.tableData.push({
            pressure: element.main.pressure,
            day: element.dayName,
            tempC: element.main.tempC,
          });
          this.loadingData = false;
          this.imageSrc = determineCurrentWeatherImage(
            this.longWeatherModel.list[0].weather[0].description
          );
          this.dataSource = this.tableData;
          this.currentWeatherDetails = this.longWeatherModel.list[0];
        });
      });
  }

  changeDayOfWeek(dayOfWeek: string): void {
    let weather = this.longWeatherModel.list.filter(
      (x) => x.dayName === dayOfWeek
    )[0];

    this.currentWeatherDetails = weather;
    this.imageSrc = determineCurrentWeatherImage(
      this.currentWeatherDetails.weather[0].description
    );
  }
}
