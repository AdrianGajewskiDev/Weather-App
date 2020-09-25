import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { ReactiveFormsModule } from "@angular/forms";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { routes } from "../app/app.routes";
import { AngularMaterialModule } from "./modules/angular-material/angular-material.module";
import { PushNotificationsModule } from "ng-push";
import { ChartsModule } from "ng2-charts";

import { WeatherService } from "./shared/services/weather.service";
import { NotificationsService } from "./shared/services/notifications.service";

import { AppComponent } from "./app.component";
import { HomeComponent } from "./home-component/home-component";
import { from, fromEventPattern } from "rxjs";
import { NavbarComponent } from "./navbar/navbar.component";
import { FooterComponent } from "./footer/footer.component";
import { WeatherComponent } from "./weather/weather.component";
import { NotificationsRegisterComponent } from "./notifications-register/notifications-register.component";
import { ComunicationService } from "./shared/services/comunication.service";
import { LongWeatherForecastComponent } from "./long-weather-forecast/long-weather-forecast.component";
import { LineChartComponent } from './line-chart/line-chart.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    FooterComponent,
    WeatherComponent,
    NotificationsRegisterComponent,
    LongWeatherForecastComponent,
    LineChartComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule,
    FormsModule,
    AngularMaterialModule,
    HttpClientModule,
    PushNotificationsModule,
    ChartsModule,
  ],
  providers: [WeatherService, NotificationsService, ComunicationService],
  bootstrap: [AppComponent],
})
export class AppModule {}
