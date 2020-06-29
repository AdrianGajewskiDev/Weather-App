import { Component, OnInit, Input } from "@angular/core";
import { FormGroup, FormBuilder } from "@angular/forms";
import { ThemePalette } from "@angular/material/core";
import { DataType } from "../shared/data.type";
import { Router } from "@angular/router";
import { slideAnimation } from "../shared/animations/animations";
import { delay } from "../shared/delay";
import { ComunicationService } from "../shared/services/comunication.service";
import { NotificationsService } from "../shared/services/notifications.service";

@Component({
  selector: "app-home-component",
  templateUrl: "./home-component.html",
  styleUrls: ["./home-component.css"],
  animations: [slideAnimation],
})
export class HomeComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private communicationService: ComunicationService,
    private notificationService: NotificationsService
  ) {}

  @Input() color: ThemePalette = "primary";
  form: FormGroup;
  state: "slideIn" | "slideOut" = "slideIn";

  private externalURl = " http://bulk.openweathermap.org/sample/";
  private dataType: DataType = DataType.CityName;
  private data: string | string[] = "";

  showError: boolean = false;
  errorMessage: string = "";
  usingUserLocation: boolean = false;

  ngOnInit(): void {
    this.form = this.fb.group({
      cityName: [""],
      cityID: [""],
      cityLongitude: [""],
      cityLatitude: [""],
    });

    this.communicationService.connect();
    this.notificationService
      .getNotification(localStorage.getItem("userID"))
      .subscribe((res) => console.log(res));
  }

  goToCityIDs(): void {
    document.location.href = this.externalURl;
  }

  async onSubmit() {
    this.dataType = this.checkDataType();

    if (this.showError == true) return;

    switch (this.dataType) {
      case DataType.CityName:
        {
          localStorage.setItem("dataType", "cityName");
          var city = this.form.get("cityName").value;
          this.state = "slideOut";
          await delay(300);
          this.router.navigateByUrl("/weather/" + city);
          return;
        }
        break;
      case DataType.CityID:
        {
          localStorage.setItem("dataType", "cityID");
          var cityID = this.form.get("cityID").value;
          this.state = "slideOut";
          await delay(300);
          this.router.navigateByUrl("/weather/" + cityID);
          return;
        }
        break;
      case DataType.LongitudeAndLatitude:
        {
          if (this.usingUserLocation == false) {
            this.useCityGeoLocation(
              this.form.get("cityLatitude").value,
              this.form.get("cityLongitude").value
            );
            return;
          }
        }
        break;
    }
  }

  getLocation(): void {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.useCityGeoLocation(
          position.coords.latitude,
          position.coords.longitude
        );
      });
    }
    this.usingUserLocation = true;
    this.dataType = DataType.LongitudeAndLatitude;
  }

  async useCityGeoLocation(lat, lon) {
    localStorage.setItem("dataType", "cityCoord");
    localStorage.setItem("lat", lat);
    localStorage.setItem("lon", lon);
    this.state = "slideOut";
    await delay(300);
    this.router.navigateByUrl("/weather");
    return;
  }

  checkDataType(): DataType {
    var input: string[] = [];

    for (const field in this.form.controls) {
      if (this.form.controls[field].value != "") {
        input.push(field);
      }
    }
    if (!(Array.isArray(input) && input.length)) {
      this.errorMessage = "Please fill one of the form inputs";
      this.showError = true;
      return DataType.Invalid;
    }

    if (input[0] == "cityLongitude" && input[1] == "cityLatitude") {
      this.data = [
        this.form.controls[input[0]].value,
        this.form.controls[input[1]].value,
      ];

      return DataType.LongitudeAndLatitude;
    }

    if (input.length > 1) {
      this.errorMessage = "Please use only one of the following";
      this.showError = true;
      return DataType.Invalid;
    }

    this.showError = false;
    this.data = this.form.controls[input[0]].value;

    switch (input[0]) {
      case "cityName":
        return DataType.CityName;
      case "cityID":
        return DataType.CityID;
    }
  }
}
