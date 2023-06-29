import {Action, createReducer, on} from '@ngrx/store';
import * as CompanyActions from './company.actions';
import {ICompanyProfile} from "../recources/models/company-profile";
import {IProjectOverview} from "../recources/models/project-overview";
import {createFormGroupState, disable, enable, FormGroupState, onNgrxForms} from "ngrx-forms";
import * as fromCompanyProfileForm from "../recources/forms/company-profile-form";
import { state } from '@angular/animations';
import { environment } from 'src/environments/environment';

export const companyFeatureKey = 'company';

export interface State {
  company: ICompanyProfile,
  companyProjects : IProjectOverview[],
  companyProfileForm : FormGroupState<fromCompanyProfileForm.CompanyProfileFormValue>
  error: any
}

export const initialState: State = {
  company: {} as ICompanyProfile,
  companyProjects : [] as IProjectOverview[],
  companyProfileForm: fromCompanyProfileForm.initialFormState,
  error: null
};

export const reducer = createReducer(
  initialState,
  onNgrxForms(),
  on(CompanyActions.loadCompanySuccess, (state, action) => {
    return {
      ...state,
      company: action.company
    }
  }),
  on(CompanyActions.loadCompanyFailure, (state, action) => {
    return {
      ...state,
      error: action.error
    }
  }),
  on(CompanyActions.loadProjectsSuccess, (state, action) => {
    return {
      ...state,
      companyProjects: action.projects
    }
  }),
  on(CompanyActions.loadProjectsFailure, (state, action) => {
    return {
      ...state,
      error: action.error
    }
  }),
  on(CompanyActions.loadDisabledCompanyProfileForm, (state) => {
    return {
      ...state,
      companyProfileForm: disable(
        createFormGroupState<fromCompanyProfileForm.CompanyProfileFormValue>(
          fromCompanyProfileForm.FORM_ID,
          {
            email: state.company.email,
            address: state.company.address,
            website: state.company.website
          }
        )
      )
    }
  }),
  on(CompanyActions.enableEditingCompanyProfileForm, (state) => {
    return {
      ...state,
      companyProfileForm: enable(state.companyProfileForm),
    }
  }),
  on(CompanyActions.cancelEditingCompanyProfileForm, (state) =>{
    return {
      ...state,
      companyProfileForm: disable(state.companyProfileForm)
    }
  }),
  on(CompanyActions.submitEditingCompanyProfileFormSuccess, (state, action) => {
    return {
      ...state,
      company: action.result,
      companyProfileForm: disable(state.companyProfileForm)
    }
  }),
  on(CompanyActions.submitEditingCompanyProfileFormFailure, (state, action) =>{
    return {
      ...state,
      error: action.error
    }
  }),
  on(CompanyActions.loadCompanyLogoSuccess, (state, action)=>{
    return{
      ...state,
      company: {
        ...state.company,
        image: action.logo.link 
      }
    }
  }),
  on(CompanyActions.loadCompanyLogoFailure, (state, action) => {
    return {
      ...state,
      company:{
        ...state.company,
        image: ''
      },
      error: action.error
    }
  }),
  on(CompanyActions.deleteCompanyLogoSuccess, (state, action)=>{
    return{
      ...state,
      company: {
        ...state.company,
        image:'' 
      }
    }
  }),
  on(CompanyActions.deleteCompanyLogoFailure, (state, action) => {
    return {
      ...state,
      error: action.error
    }
  }),
);
