import { Component, OnInit, Input } from "@angular/core";
import { FormGroup, FormBuilder } from "@angular/forms";
import { ThemePalette } from "@angular/material/core";
import { DataType } from "../shared/data.type";
import { Router } from "@angular/router";
import { slideAnimation } from "../shared/animations/animations";
import { delay } from "../shared/delay";

@Component({
  selector: "app-home-component",
  templateUrl: "./home-component.html",
  styleUrls: ["./home-component.css"],
  animations: [slideAnimation],
})
export class HomeComponent implements OnInit {
  constructor(private fb: FormBuilder, private router: Router) {}

  @Input() color: ThemePalette = "primary";
  form: FormGroup;
  state: "slideIn" | "slideOut" = "slideIn";

  private externalURl = " http://bulk.openweathermap.org/sample/";
  private dataType: DataType = DataType.CityName;
  private data: string | string[] = "";

  showError: boolean = false;
  errorMessage: string = "";

  ngOnInit(): void {
    this.form = this.fb.group({
      cityName: [""],
      cityID: [""],
      cityLongitude: [""],
      cityLatitude: [""],
    });
  }

  goToCityIDs(): void {
    document.location.href = this.externalURl;
  }

  async onSubmit() {
    this.dataType = this.checkDataType();

    if (this.showError == true) return;
    1;
    console.log(this.data);

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
    }
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
