import { Component, OnInit } from "@angular/core";
import { ComunicationService } from "./shared/services/comunication.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent implements OnInit {
  constructor(private comunicationService: ComunicationService) {}

  ngOnInit(): void {
    if (this.comunicationService.connect())
      this.comunicationService.setSignalRListener("socket", (message) =>
        console.log(message)
      );
  }

  title = "WeatherApp";
}
