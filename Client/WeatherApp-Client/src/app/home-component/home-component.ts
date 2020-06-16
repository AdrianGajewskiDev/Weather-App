import { Component, OnInit, Input } from "@angular/core";
import { FormGroup, FormBuilder } from "@angular/forms";
import { ThemePalette } from "@angular/material/core";
import { DataType } from "../shared/data.type";
import {
  trigger,
  state,
  style,
  animate,
  transition,
  // ...
} from "@angular/animations";

@Component({
  selector: "app-home-component",
  templateUrl: "./home-component.html",
  styleUrls: ["./home-component.css"],
  animations: [
    trigger("slideInOut", [
      state(
        "slideIn",
        style({
          transform: "translateX(0)",
        })
      ),
      state(
        "slideOut",
        style({
          transform: "translateX(300%)",
        })
      ),
      transition("* => slideIn", [animate("0.3s")]),
      transition("* => slideOut", [animate("0.3s")]),
    ]),
  ],
})
export class HomeComponent implements OnInit {
  constructor(private fb: FormBuilder) {}

  @Input() color: ThemePalette = "primary";
  form: FormGroup;
  state: "slideIn" | "slideOut" = "slideIn";

  private externalURl = " http://bulk.openweathermap.org/sample/";
  private dataType: DataType = DataType.CityName;

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

  onSubmit(): void {
    this.state = "slideOut";
  }
}
