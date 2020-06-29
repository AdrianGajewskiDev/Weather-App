import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";

@Injectable()
export class ComunicationService {
  constructor() {
    this.hub = new signalR.HubConnectionBuilder()
      .withUrl(this.hubUrl)
      .configureLogging(signalR.LogLevel.Debug)
      .build();
  }

  private hubUrl = "https://localhost:44377/notificationsHub";
  private hub: signalR.HubConnection;

  get GetConnectionID(): string {
    return localStorage.getItem("connectionID");
  }

  setConnectionID(connectionID: string): void {
    localStorage.setItem("connectionID", connectionID);
  }

  removeConnectionID(): void {
    localStorage.removeItem("connectionID");
  }

  connect() {
    let userID = localStorage.getItem("userID");

    if (userID === null || userID == undefined) return;

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
    this.hub.on("socket", (message: string) => console.log(message));
  }

  disconnect() {
    this.hub.stop();
    this.removeConnectionID;
  }
}
