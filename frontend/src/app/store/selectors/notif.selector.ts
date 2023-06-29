import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromNotifs from '../reducers/notifi.reducer';

export const selectNotifsState = createFeatureSelector<fromNotifs.State>(
  fromNotifs.notifiFeatureKey
);

export const selectNotificationsList = createSelector(
  selectNotifsState,
  (state) => state.notifications
);

export const selectNotificationsCount = createSelector(
  selectNotifsState,
  (state) => state.notifCount
)
