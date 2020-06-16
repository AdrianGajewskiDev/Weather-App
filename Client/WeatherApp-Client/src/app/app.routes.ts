import { Routes } from "@angular/router";
import { HomeComponent } from "./home-component/home-component";
import { WeatherComponent } from "./weather/weather.component";

export const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
  },
  {
    path: "weather/:details",
    component: WeatherComponent,
  },
];
