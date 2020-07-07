import { Component, OnInit, Input } from "@angular/core";
import { slideAnimation } from "../shared/animations/animations";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ThemePalette } from "@angular/material/core";
import { NotificationRequestModel } from "../shared/models/notificationRequestModel";
import { NotificationsService } from "../shared/services/notifications.service";
import { ComunicationService } from "../shared/services/comunication.service";

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
    private comunicationService: ComunicationService
  ) {}

  state: "slideIn" | "slideOut" = "slideIn";
  @Input() color: ThemePalette = "primary";
  form: FormGroup;

  testUserId = this.notificationsService.GetUserID;
  testUserConnectionID = this.comunicationService.GetConnectionID;

  ngOnInit(): void {
    if (this.comunicationService.Connected == false) {
      this.comunicationService.connect();
      this.comunicationService.setSignalRListener("socket");
    }

    this.form = this.fb.group({
      Email: ["", [Validators.required, Validators.email]],
      CityName: ["", Validators.required],
    });
    console.log(`Calling from id: ${localStorage.getItem("userID")}`);

    if (this.notificationsService.GetUserID != null || undefined)
      this.notificationsService.getNotification().subscribe();
  }

  onSubmit(): void {
    let model: NotificationRequestModel = {
      UserEmail: this.form.get("Email").value,
      RequestedCityName: this.form.get("CityName").value,
    };

    this.notificationsService
      .registerToNotifications(model)
      .subscribe((res: any) => {
        localStorage.setItem("userID", res.userID);
        console.log("OK");
      });
  }
}
