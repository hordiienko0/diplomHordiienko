import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromAdministration from './administration.reducer';

export const selectAdministrationState =
  createFeatureSelector<fromAdministration.State>(
    fromAdministration.administrationFeatureKey
  );

export const selectCurrentlyOpenCompany = createSelector(
  selectAdministrationState,
  (state) => state.currentlyOpenCompany
);

export const selectCompanies = createSelector(
  selectAdministrationState,
  (state) => state.companies
);

export const selectCompaniesParams = createSelector(
  selectAdministrationState,
  (state) => state.companiesParams
);

export const selectRoles = createSelector(
  selectAdministrationState,
  (state) => state.roles
);

export const selectCompanyInformationForm = createSelector(
  selectAdministrationState,
  (state) => state.companyInformationForm
);
export const selectUserDetails = createSelector(
  selectAdministrationState,
  (state) => state.currentUserDetails
);
export const selectUserDetailsCompanyId = createSelector(
  selectUserDetails,
  (details) => details?.companyId
);
export const selectFormEnabled = createSelector(
  selectCompanyInformationForm,
  (form) => form.isEnabled
);
export const selectErrorLines = createSelector(
  selectAdministrationState,
  (state)=> state.failedLines
);
export const selectCompanyId = createSelector(
  selectAdministrationState,
  (state)=> state.currentlyOpenCompany.id
)
export const selectNewCompanyId = createSelector(
  selectAdministrationState,
  (state) => state.newCompanyId
)

export const selectCompanyProjectsParams = createSelector(
  selectAdministrationState,
  (state) => state.companyProjectsParams
)