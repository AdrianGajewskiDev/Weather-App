import { Component, OnInit, Input } from "@angular/core";
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
} from "@angular/forms";
import { ThemePalette } from "@angular/material/core";
import { TooltipPosition } from "@angular/material/tooltip";
import { DataType } from "../shared/data.type";

@Component({
  selector: "app-home-component",
  templateUrl: "./home-component.html",
  styleUrls: ["./home-component.css"],
})
export class HomeComponent implements OnInit {
  constructor(private fb: FormBuilder) {}
  @Input()
  color: ThemePalette = "primary";
  positionOptions: TooltipPosition[] = [
    "after",
    "before",
    "above",
    "below",
    "left",
    "right",
  ];
  position = new FormControl(this.positionOptions[0]);
  form: FormGroup;

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
    console.log(this.form);
  }
}
