import { WeatherImagesUrls } from "./weatherImagesUrls";

export class WeatherDescriptions {
  public static Thunderstorm = "Thunderstorm";
  public static Drizzle = "Drizzle";
  public static Rain = "Rain";
  public static Mist = "Mist";
  public static Smoke = "Smoke";
  public static Haze = "Haze";
  public static Fog = "Fog";
  public static Sand = "Sand";
  public static Dust = "Dust";
  public static Ash = "Ash";
  public static Squall = "Squall";
  public static Tornado = "Tornado";
  public static Clear = "Clear";
  public static Clouds = "Clouds";
}

export function determineCurrentWeatherImage(main: string): string {
  switch (main) {
    case WeatherDescriptions.Thunderstorm:
      return WeatherImagesUrls.storm;
    case WeatherDescriptions.Drizzle:
      return WeatherImagesUrls.drizzle;
    case WeatherDescriptions.Rain:
      return WeatherImagesUrls.rainWOSun;
    case WeatherDescriptions.Mist:
      return WeatherImagesUrls.mist;
    case WeatherDescriptions.Smoke:
      return WeatherImagesUrls.mist;
    case WeatherDescriptions.Haze:
      return WeatherImagesUrls.mist;
    case WeatherDescriptions.Fog:
      return WeatherImagesUrls.mist;
    case WeatherDescriptions.Sand:
      return WeatherImagesUrls.mist;
    case WeatherDescriptions.Dust:
      return WeatherImagesUrls.mist;
    case WeatherDescriptions.Ash:
      return WeatherImagesUrls.mist;
    case WeatherDescriptions.Squall:
      return WeatherImagesUrls.mist;
    case WeatherDescriptions.Tornado:
      return WeatherImagesUrls.storm;
    case WeatherDescriptions.Clear:
      return WeatherImagesUrls.sunny;
    case WeatherDescriptions.Clouds:
      return WeatherImagesUrls.cloudsAll;
  }
}
