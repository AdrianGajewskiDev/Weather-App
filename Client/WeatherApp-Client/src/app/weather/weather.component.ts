import { Component, OnInit } from "@angular/core";

import { ActivatedRoute, Router } from "@angular/router";
import { WeatherModel } from "../shared/models/weather.model";
import { WeatherService } from "../shared/services/weather.service";
import { slideAnimation } from "../shared/animations/animations";
import { delay } from "../shared/delay";
import Coord from "../shared/models/Coord";

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
  lat?: number;
  lon?: number;
  windDegImageUrl: string = "";

  ngOnInit(): void {
    if (this.data == null) {
      this.lat = parseFloat(localStorage.getItem("lat"));
      this.lon = parseFloat(localStorage.getItem("lon"));
    }

    switch (this.dataType) {
      case "cityName": {
        this.weatherService.getWeatherByCity(this.data).subscribe(
          (res) => {
            this.weatherDetails = res.responseBody;
            this.showLoadingSpinner = false;
            this.showError = false;
            this.checkWindDeg();
          },
          (error) => {
            this.showError = true;

            this.showLoadingSpinner = false;
          }
        );
      }
      case "cityID": {
        this.weatherService.getWeatherByCityID(parseInt(this.data)).subscribe(
          (res) => {
            this.weatherDetails = res.responseBody;
            this.showLoadingSpinner = false;
            this.showError = false;

            this.checkWindDeg();
          },
          (error) => {
            this.showError = true;
          }
        );
      }
      case "cityCoord": {
        this.weatherService
          .getWeatherByCityCoord(new Coord(this.lon, this.lat))
          .subscribe(
            (res) => {
              this.weatherDetails = res.responseBody;
              this.showLoadingSpinner = false;
              this.showError = false;

              this.checkWindDeg();
            },
            (error) => {
              this.showError = true;
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

  checkWindDeg(): void {
    if (this.weatherDetails.wind.deg <= 45) {
      this.windDegImageUrl = "../../assets/Images/weatherIcons/wind45deg.png";
      return;
    }
    if (
      this.weatherDetails.wind.deg > 45 &&
      this.weatherDetails.wind.deg <= 90
    ) {
      this.windDegImageUrl = "../../assets/Images/weatherIcons/wind90deg.png";
      return;
    }
    if (
      this.weatherDetails.wind.deg > 90 &&
      this.weatherDetails.wind.deg <= 135
    ) {
      this.windDegImageUrl = "../../assets/Images/weatherIcons/wins135deg.png";
      return;
    }
    if (
      this.weatherDetails.wind.deg > 135 &&
      this.weatherDetails.wind.deg <= 180
    ) {
      this.windDegImageUrl = "../../assets/Images/weatherIcons/wind180deg.png";
      return;
    }
    if (
      this.weatherDetails.wind.deg > 180 &&
      this.weatherDetails.wind.deg <= 225
    ) {
      this.windDegImageUrl = "../../assets/Images/weatherIcons/wind225deg.png";
      return;
    }

    if (
      this.weatherDetails.wind.deg > 225 &&
      this.weatherDetails.wind.deg <= 270
    ) {
      this.windDegImageUrl = "../../assets/Images/weatherIcons/wind270deg.png";
      return;
    }

    if (
      this.weatherDetails.wind.deg > 270 &&
      this.weatherDetails.wind.deg <= 315
    ) {
      this.windDegImageUrl = "../../assets/Images/weatherIcons/wind305deg.png";
      return;
    }

    if (
      this.weatherDetails.wind.deg > 315 &&
      this.weatherDetails.wind.deg <= 360
    ) {
      this.windDegImageUrl = "../../assets/Images/weatherIcons/wind360deg.png";
      return;
    }
  }
}
