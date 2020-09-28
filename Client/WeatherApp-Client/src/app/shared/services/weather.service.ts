import { Injectable } from "@angular/core";
import { HttpClient, HttpParams, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { WeatherModel } from "../models/weather.model";
import { Response } from "../models/response";
import Coord from "../models/Coord";
import { LongWeatherForecastModel } from "../models/longWeatherForecastModel";

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

  getWeatherByCityCoord(coord: Coord): Observable<Response<WeatherModel>> {
    return this.httpClient.get<Response<WeatherModel>>(
      this.baseUrl + "currentweatherByCityCoord",
      {
        params: {
          lat: coord.lat.toString(),
          lon: coord.lon.toString(),
        },
      }
    );
  }

  get6DaysWeatherForecast(
    cityName: string
  ): Observable<Response<LongWeatherForecastModel>> {
    return this.httpClient.get<Response<LongWeatherForecastModel>>(
      this.baseUrl + "longWeatherForecast/" + cityName
    );
  }
}
