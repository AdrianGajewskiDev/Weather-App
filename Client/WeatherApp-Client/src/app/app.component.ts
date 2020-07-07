import { Component, OnInit } from "@angular/core";
import { ComunicationService } from "./shared/services/comunication.service";
import { Weather } from "./shared/models/Weather";
import { WeatherNotificationModel } from "./shared/models/weatherNotification.model";
import { NotificationsService } from "./shared/services/notifications.service";
import { WeatherModel } from "./shared/models/weather.model";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent implements OnInit {
  constructor(
    private comunicationService: ComunicationService,
    private notificationService: NotificationsService
  ) {}

  ngOnInit(): void {
    console.log(`User id : ${this.notificationService.GetUserID}`);
    console.log(
      `User connection id : ${this.comunicationService.GetConnectionID}`
    );

    if (this.comunicationService.connect()) {
      console.log("Connected successfully!!!");
      this.comunicationService.setSignalRListener("socket");
    }
  }

  title = "Weatherealise";
}
