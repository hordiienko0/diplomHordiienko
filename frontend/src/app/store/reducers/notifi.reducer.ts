import { createReducer, on } from '@ngrx/store';
import { INotification } from 'src/app/modules/notifications/resources/models/notification.model';
import * as fromNotifActions from "../actions/notifi.actions";


export const notifiFeatureKey = 'notifi';


export interface State {
  notifications: INotification[],
  notifCount: number;
}

export const initialState: State = {
  notifications: [] as INotification[],
  notifCount: 0
};

export const reducer = createReducer(
  initialState,
  on(fromNotifActions.loadNotifisSuccess, (state, notifs) => {
    return {
      ...state,
      notifications: notifs.notifications,
      notifCount: notifs.notifications.length
    }
  })
)
