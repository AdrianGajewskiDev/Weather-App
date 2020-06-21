import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { WeatherModel } from "../models/weather.model";
import { Response } from "../models/response";

@Injectable()
export class WeatherService {
  constructor(private httpClient: HttpClient) {}

  private baseUrl: string = "https://localhost:44377/api/weather/";

  getWeatherByCity(city: string): Observable<Response<WeatherModel>> {
    return this.httpClient.get<Response<WeatherModel>>(
      this.baseUrl + "currentweatherByCity/" + city
    );
  }

  getWeatherByCityID(cityID: number): Observable<Response<WeatherModel>> {
    return this.httpClient.get<Response<WeatherModel>>(
      this.baseUrl + "currentweatherByCityID/" + cityID
    );
  }
}
