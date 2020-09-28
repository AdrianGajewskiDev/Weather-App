import { Clouds } from "./Clouds";
import { Main } from "./main";
import { Weather } from "./Weather";
import { Wind } from "./Wind";

export class LongWeatherForecastListItemModel {
  main: Main;
  wind: Wind;
  weather: Weather[];
  clouds: Clouds;
  dt_txt: string;
  dayName: string;
  weatherForecastDateTime: Date;
}
