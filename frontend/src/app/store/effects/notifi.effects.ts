import { Injectable } from "@angular/core";
import { Actions, createEffect, Effect, ofType } from "@ngrx/effects";
import { catchError, map, mergeMap, of } from "rxjs";
import { NotifiApiService } from "../../modules/notifications/resources/services/notifi-api.service";
import * as fromNotifiActions from '../actions/notifi.actions';

@Injectable()
export class NotificationsEffects {
  loadArticles$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromNotifiActions.loadNotifis),
        mergeMap((action) =>
          this.notifiApiService.getNotificationsForUser().pipe(
            map(
              (notifis) =>
                fromNotifiActions.loadNotifisSuccess({
                  notifications: notifis
                })
            ), catchError(() => of(fromNotifiActions.loadNotifisFailure({ error : "Not found"})))
          )
        )
      ), 
  );

  deleteNotif$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromNotifiActions.deleteNotifi),
        mergeMap((action) =>
          this.notifiApiService.deleteNotification(action.id).pipe(
            map(() =>
              fromNotifiActions.loadNotifis()
            )
          )
          )
      )
  )

  deleteAllNotif$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromNotifiActions.deleteAllNotifi),
        mergeMap((action) =>
          this.notifiApiService.deleteAllNotifications(action.userId).pipe(
            map(() =>
              fromNotifiActions.loadNotifis()
            ), catchError((err) => of(fromNotifiActions.deleteAllNotifiFailure))
          )
        )
      )
  )

  constructor(private actions$: Actions, private notifiApiService: NotifiApiService) { }
}
