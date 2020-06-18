import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { WeatherModel } from "../models/weather.model";

@Injectable()
export class WeatherService {
  constructor(private httpClient: HttpClient) {}

  private baseUrl: string = "https://localhost:44377/api/weather/";

  getWeatherByCity(city: string): Observable<WeatherModel> {
    return this.httpClient.get<WeatherModel>(
      this.baseUrl + "currentweatherByCity/" + city
    );
  }
}
