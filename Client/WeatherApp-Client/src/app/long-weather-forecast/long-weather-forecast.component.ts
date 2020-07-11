import { Component, OnInit } from "@angular/core";
import { slideAnimation } from "../shared/animations/animations";

@Component({
  selector: "app-long-weather-forecast",
  templateUrl: "./long-weather-forecast.component.html",
  styleUrls: ["./long-weather-forecast.component.css"],
})
export class LongWeatherForecastComponent implements OnInit {
  constructor() {}
  state: "slideIn" | "slideOut" = "slideIn";
  ngOnInit(): void {}
}
