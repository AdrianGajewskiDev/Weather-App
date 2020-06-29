import { Component, OnInit, Input } from "@angular/core";
import { slideAnimation } from "../shared/animations/animations";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ThemePalette } from "@angular/material/core";
import { NotificationRequestModel } from "../shared/models/notificationRequestModel";
import { NotificationsService } from "../shared/services/notifications.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-notifications-register",
  templateUrl: "./notifications-register.component.html",
  styleUrls: ["./notifications-register.component.css"],
  animations: [slideAnimation],
})
export class NotificationsRegisterComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    private notificationsService: NotificationsService,
    private router: Router
  ) {}

  state: "slideIn" | "slideOut" = "slideIn";
  @Input() color: ThemePalette = "primary";
  form: FormGroup;

  ngOnInit(): void {
    this.form = this.fb.group({
      Email: ["", [Validators.required, Validators.email]],
      CityName: ["", Validators.required],
    });
  }

  onSubmit(): void {
    let model: NotificationRequestModel = {
      UserEmail: this.form.get("Email").value,
      RequestedCityName: this.form.get("CityName").value,
    };

    this.notificationsService
      .registerToNotifications(model)
      .subscribe((res) => {
        console.log("success!!!");
      });
  }
}
