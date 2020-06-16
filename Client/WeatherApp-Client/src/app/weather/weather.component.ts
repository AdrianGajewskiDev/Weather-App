import { Component, OnInit } from "@angular/core";
import {
  trigger,
  state,
  style,
  transition,
  animate,
} from "@angular/animations";
import { ActivatedRoute } from "@angular/router";
import { WeatherModel } from "../shared/models/weather.model";
import { WeatherService } from "../shared/services/weather.service";

@Component({
  selector: "app-weather",
  templateUrl: "./weather.component.html",
  styleUrls: ["./weather.component.css"],
  animations: [
    trigger("slideInOut", [
      state(
        "slideIn",
        style({
          transform: "translateX(0)",
        })
      ),
      state(
        "slideOut",
        style({
          transform: "translateX(300%)",
        })
      ),
      transition("* => slideIn", [animate("0.3s")]),
      transition("* => slideOut", [animate("0.3s")]),
    ]),
  ],
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

  ngOnInit(): void {
    this.weatherService.getWeatherByCity(this.data).subscribe(
      (res) => {
        this.weatherDetails.main = res.main;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
