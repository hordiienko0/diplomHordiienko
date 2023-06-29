import { createAction, props } from "@ngrx/store";
import { HttError } from "src/app/modules/error/resources/models/httpError";
import { INotification } from "../../modules/notifications/resources/models/notification.model";
import { User } from "../reducers/auth.reducer";

export const loadNotifis = createAction(
  '[Notifi] Load Notifi'
);

export const loadNotifisSuccess = createAction(
  '[Notifi] Load Notifi Success',
  props<{ notifications: INotification[] }>()
);

export const loadNotifisFailure = createAction(
  '[Notifi] Load Notifi Failure',
  props<{ error: any }>()
);

export const deleteNotifi = createAction( 
  '[Notifi] Delete Notifi',
  props<{ id: number, userId: number}>()
);

export const deleteNotifiSuccess = createAction(
  '[Notifi] Delete Notifi Success'
);

export const deleteNotifiFailure = createAction(
  '[Notifi] Delete Notifi Failure'
);

export const deleteAllNotifi = createAction(
  '[Notifi] Delete All Notifi',
  props<{ userId: number }>()
)

export const deleteAllNotifiSuccess = createAction(
  '[Notifi] Delete All Notifi Success'
);

export const deleteAllNotifiFailure = createAction(
  '[Notifi] Delete All Notifi Failure'
);
