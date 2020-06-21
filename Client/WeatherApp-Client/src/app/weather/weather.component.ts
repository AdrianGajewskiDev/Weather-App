import { Component, OnInit } from "@angular/core";

import { ActivatedRoute, Router } from "@angular/router";
import { WeatherModel } from "../shared/models/weather.model";
import { WeatherService } from "../shared/services/weather.service";
import { slideAnimation } from "../shared/animations/animations";
import { delay } from "../shared/delay";

@Component({
  selector: "app-weather",
  templateUrl: "./weather.component.html",
  styleUrls: ["./weather.component.css"],
  animations: [slideAnimation],
})
export class WeatherComponent implements OnInit {
  constructor(
    private routes: ActivatedRoute,
    private weatherService: WeatherService,
    private router: Router
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
  showError: boolean = false;

  ngOnInit(): void {
    switch (this.dataType) {
      case "cityName": {
        this.weatherService.getWeatherByCity(this.data).subscribe(
          (res) => {
            this.weatherDetails = res.responseBody;
            this.showLoadingSpinner = false;
          },
          (error) => {
            if (error.statusCode == 404) this.showError = true;
          }
        );
      }
      case "cityID": {
        this.weatherService.getWeatherByCityID(parseInt(this.data)).subscribe(
          (res) => {
            this.weatherDetails = res.responseBody;
            this.showLoadingSpinner = false;
          },
          (error) => {
            if (error.statusCode == 404) this.showError = true;
          }
        );
      }
    }
  }

  async goBack() {
    this.state = "slideOut";
    await delay(300);
    this.router.navigateByUrl("/home");
  }
}
