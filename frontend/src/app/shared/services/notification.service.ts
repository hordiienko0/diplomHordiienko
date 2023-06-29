import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import * as signalR from '@microsoft/signalr';
import { NotificationDto } from "../models/notification-dto.model";
import { User } from "../../store/reducers/auth.reducer";
import { select, Store } from "@ngrx/store";
import { AppState } from "../../store";
import { selectUser } from "../../store/selectors/auth.selectors";
import { connect, map, Observable, tap } from "rxjs";
import { number } from "ngrx-forms/validation";
import { AlertService } from "../../modules/alert/resources/services/alert.service";
import * as fromNotifiActions from "../../store/actions/notifi.actions"

@Injectable({
  providedIn: "root",
})
export class NotificationService {
  started: boolean = false;
  private connection: signalR.HubConnection | undefined
  curentUser$: Observable<User | null>;
  UserId: number | undefined;

  constructor(private store: Store<AppState>, private alertService: AlertService) {
    this.curentUser$ = this.store.pipe(select(selectUser));
    this.curentUser$.subscribe(el => this.UserId = el?.id);
  }

  startService() {
    console.log("UserId: " + this.UserId);
    if (this.started || this.UserId == undefined) return;
    console.log("Start creating NotifService");
    this.connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl(environment.apiHostWithHttp + '/notification?userid=' + this.UserId)
      .build();
    this.connection.start().then(function () {
      console.log('SignalR Connected!');
    }).catch(function (err) {
      return console.error(err.toString());
    });

    this.addHendlers();
    if (this.UserId != undefined) {
      this.store.dispatch(fromNotifiActions.loadNotifis());
    }
    this.started = true;
  }

  addHendlers = () => {
    if (this.connection != undefined) {
      this.connection.on("SendNotif", (notif: NotificationDto) => {
        console.log(notif.type);
        this.alertService.showAlert(notif.message, "OK", notif.type)
        if (this.UserId != undefined) {
          this.store.dispatch(fromNotifiActions.loadNotifis());
        }
      }); 
    }
  }

  UpdateConnection() {
    if (this.UserId != undefined && !this.started) {
      this.startService();
    }
  }
  

  stopService() {
    console.log("UserId: " + this.UserId);
    console.log("SignalR Disconnected.");
    if (this.connection != undefined) {
      this.connection.stop();
      this.connection = undefined;
    }
    this.started = false;
    this.UserId = undefined;
  }

  deleteNotif(notiid: number) {
    if (this.UserId != undefined)
    this.store.dispatch(fromNotifiActions.deleteNotifi({ id: notiid, userId: this.UserId }));
  }

  deleteAllNotifi() {
    if (this.UserId != undefined)
      this.store.dispatch(fromNotifiActions.deleteAllNotifi({ userId: this.UserId }));
  }
}
