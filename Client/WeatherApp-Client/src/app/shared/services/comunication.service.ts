import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { WeatherNotificationModel } from "../models/weatherNotification.model";
import { NotificationsService } from "./notifications.service";
import { determineCurrentWeatherImage } from "../weatherConditions/weatherDescriptions";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class ComunicationService {
  constructor(
    private notificationsService: NotificationsService,
    private client: HttpClient
  ) {
    this.hub = new signalR.HubConnectionBuilder()
      .withUrl(this.hubUrl)
      .configureLogging(signalR.LogLevel.Information)
      .build();
  }

  private hubUrl = "https://localhost:44377/notificationsHub";
  private hub: signalR.HubConnection;

  public Connected: boolean = false;

  get GetConnectionID(): string {
    return localStorage.getItem("connectionID");
  }

  setConnectionID(connectionID: string): void {
    localStorage.setItem("connectionID", connectionID);
  }

  removeConnectionID(): void {
    localStorage.removeItem("connectionID");
  }

  tryGetConnectionID(userID) {
    console.log("Getting");

    let url = `https://localhost:44377/api/Notifications/getUserID/${userID}`;
    return this.client.get(url);
  }

  connect(): boolean {
    let userID = localStorage.getItem("userID");

    if (userID === null || userID == undefined) {
      this.removeConnectionID();
      return false;
    }
    this.hub.start().then(() =>
      this.hub
        .invoke("GetConnectionID", userID)
        .then((res) => {
          this.setConnectionID(res);
        })
        .catch(function (err) {
          console.error(err.toString());
        })
    );

    this.Connected = true;
    console.log(this.hub);

    return true;
  }

  setSignalRListener(func): void {
    console.log("Setting up listener....");

    this.hub.on(func, (res) => {
      console.log("here");

      let model: WeatherNotificationModel = {
        CityName: res.responseBody.name,
        ImageUrl: determineCurrentWeatherImage(
          res.responseBody.weather[0].main
        ),
        TempC: res.responseBody.main.tempC,
        WeatherDescription: res.responseBody.weather[0].description,
      };

      this.notificationsService.showNotification(model).subscribe(() => {
        console.log("showing");
      });
    });
  }

  disconnect() {
    this.Connected = false;

    this.hub.stop();
    this.removeConnectionID;
  }
}
