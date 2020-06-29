import { Component, OnInit, Input } from "@angular/core";
import { slideAnimation } from "../shared/animations/animations";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ThemePalette } from "@angular/material/core";

@Component({
  selector: "app-notifications-register",
  templateUrl: "./notifications-register.component.html",
  styleUrls: ["./notifications-register.component.css"],
  animations: [slideAnimation],
})
export class NotificationsRegisterComponent implements OnInit {
  constructor(private fb: FormBuilder) {}

  state: "slideIn" | "slideOut" = "slideIn";
  @Input() color: ThemePalette = "primary";
  form: FormGroup;

  ngOnInit(): void {
    this.form = this.fb.group({
      Email: ["", [Validators.required, Validators.email]],
      CityName: ["", Validators.required],
    });
  }
}
