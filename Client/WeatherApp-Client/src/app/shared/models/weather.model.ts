import { Main } from "./main";
import { Weather } from "./Weather";
import { Wind } from "./Wind";
import { Clouds } from "./Clouds";
import { Sys } from "./Sys";
import Coord from "./Coord";

export class WeatherModel {
  coord: Coord;
  main: Main;
  weather: Weather[];
  wind: Wind;
  clouds: Clouds;
  sys: Sys;
  visibility: number;
  dt: number;
  id: number;
  name: string;
}
