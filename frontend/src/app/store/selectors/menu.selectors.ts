import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromMenu from '../reducers/menu.reducer';

export const selectMenuState = createFeatureSelector<fromMenu.State>(
  fromMenu.menuFeatureKey
);

export const selectIfMenuIsOpened = createSelector(
  selectMenuState,
  (state) => state.opened
);

export const selectIfMenuIsRevealed = createSelector(
  selectMenuState,
  (state) => state.revealed
);

export const selectMenuLinks = createSelector(
  selectMenuState,
  (state) => state.links
);

export const selectMenuLogo = createSelector(
  selectMenuState,
  (state) => state.logo
)
