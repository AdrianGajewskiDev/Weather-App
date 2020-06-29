import { Routes } from "@angular/router";
import { HomeComponent } from "./home-component/home-component";
import { WeatherComponent } from "./weather/weather.component";
import { NotificationsRegisterComponent } from "./notifications-register/notifications-register.component";

export const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
  },
  {
    path: "home",
    component: HomeComponent,
  },
  {
    path: "weather/:details",
    component: WeatherComponent,
  },
  {
    path: "weather",
    component: WeatherComponent,
  },
  {
    path: "registerNotification",
    component: NotificationsRegisterComponent,
  },
];
