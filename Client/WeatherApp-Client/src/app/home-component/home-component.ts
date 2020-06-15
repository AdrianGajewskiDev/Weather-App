import { Component, OnInit, Input } from "@angular/core";
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
} from "@angular/forms";
import { ThemePalette } from "@angular/material/core";
import { TooltipPosition } from "@angular/material/tooltip";
import { Router } from "@angular/router";

@Component({
  selector: "app-home-component",
  templateUrl: "./home-component.html",
  styleUrls: ["./home-component.css"],
})
export class HomeComponent implements OnInit {
  constructor(private fb: FormBuilder) {}
  @Input()
  color: ThemePalette = "primary";
  form: FormGroup;
  positionOptions: TooltipPosition[] = [
    "after",
    "before",
    "above",
    "below",
    "left",
    "right",
  ];
  position = new FormControl(this.positionOptions[0]);

  private externalURl = " http://bulk.openweathermap.org/sample/";

  ngOnInit(): void {
    this.form = this.fb.group({
      cityFormControll: [""],
    });
  }

  goToCityIDs(): void {
    document.location.href = this.externalURl;
  }
}
