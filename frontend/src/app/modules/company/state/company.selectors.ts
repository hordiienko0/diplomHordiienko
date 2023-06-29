import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromCompany from './company.reducer';

export const selectCompanyState = createFeatureSelector<fromCompany.State>(
  fromCompany.companyFeatureKey
);

export const selectCompany = createSelector(
  selectCompanyState,
  (state) => state.company
);

export const selectProjects = createSelector(
  selectCompanyState,
  (state) => state.companyProjects
);

export const selectCompanyProfileForm = createSelector(
  selectCompanyState,
  (state) => state.companyProfileForm
)
