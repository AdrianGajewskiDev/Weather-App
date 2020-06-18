import { Component, OnInit } from "@angular/core";

import { ActivatedRoute } from "@angular/router";
import { WeatherModel } from "../shared/models/weather.model";
import { WeatherService } from "../shared/services/weather.service";
import { slideAnimation } from "../shared/animations/animations";

@Component({
  selector: "app-weather",
  templateUrl: "./weather.component.html",
  styleUrls: ["./weather.component.css"],
  animations: [slideAnimation],
})
export class WeatherComponent implements OnInit {
  constructor(
    private routes: ActivatedRoute,
    private weatherService: WeatherService
  ) {
    this.routes.params.subscribe((res) => {
      this.data = res["details"];
    });
  }

  state: "slideIn" | "slideOut" = "slideIn";
  data: string;
  weatherDetails: WeatherModel = new WeatherModel();
  dataType = localStorage.getItem("dataType");
  showLoadingSpinner: boolean = true;

  ngOnInit(): void {
    switch (this.dataType) {
      case "cityName": {
        this.weatherService.getWeatherByCity(this.data).subscribe(
          (res) => {
            this.weatherDetails = res;
            this.showLoadingSpinner = false;
          },
          (error) => {
            console.log(error);
          }
        );
      }
    }
  }
}
