import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { NotificationRequestModel } from "../models/notificationRequestModel";

@Injectable()
export class NotificationsService {
  constructor(private httpClient: HttpClient) {}

  private baseUrl: string = "https://localhost:44377/api/notifications/";

  registerToNotifications(model: NotificationRequestModel) {
    return this.httpClient.post(this.baseUrl + "registerNotification", model);
  }

  ///this is temporary method to test connection to the server
  getNotification(userID: string) {
    return this.httpClient.get(this.baseUrl + "send/" + userID);
  }
}
