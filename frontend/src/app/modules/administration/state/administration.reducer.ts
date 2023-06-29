import { createReducer, on } from '@ngrx/store';
import * as AdministrationActions from './administration.actions';
import { ICompanyOverview } from '../resources/models/company-overview.model';
import { IMember } from '../resources/models/member.model';
import { state } from '@angular/animations';
import { ICompanyDetailed } from '../resources/models/company-detailed.model';
import * as fromCompanyInformationForm from '../resources/forms/company-information-form';
import {
  createFormGroupState,
  disable,
  enable,
  FormGroupState,
  onNgrxForms,
  onNgrxFormsAction,
  SetValueAction,
} from 'ngrx-forms';
import { IRole } from '../resources/models/role.model';
import { UserDetailsDto } from '../resources/models/userDetailsDto';
import { ICompanyProject } from '../resources/models/company-project.model';
import { selectCurrentlyOpenCompany } from './administration.selectors';
import { Params } from '../resources/models/params.model';
import { Order } from '../../project/resources/models/order';

export const administrationFeatureKey = 'administration';

export interface State {
  currentUserDetails:UserDetailsDto| null;
  companiesParams: {
    filter: string,
    sort: string,
  },
  companyProjectsParams: Params
  companies: ICompanyOverview[] | null;
  currentlyOpenCompany: ICompanyDetailed;
  companyInformationForm: FormGroupState<fromCompanyInformationForm.CompanyInformationFormValue>;
  error: any;
  roles: IRole[]|null;
  failedLines: string[] | undefined;
  newCompanyId: number;
}

export const initialState: State = {
  currentUserDetails:null,
  companiesParams: {
    filter: '',
    sort: '',
  },
  companies: null,
  companyProjectsParams: {
    page: 1,
    count: 10,
    order: Order.ASC,
    sort: "Id",
  } as Params,
  currentlyOpenCompany: {
    members: [] as IMember[],
    projects: [] as ICompanyProject[],
    projectsTotalCount: 0
  } as ICompanyDetailed,
  companyInformationForm: fromCompanyInformationForm.initialFormState,
  error: null,
  roles: null,
  failedLines: undefined,
  newCompanyId: 0
};

export const reducer = createReducer(
  initialState,
  onNgrxForms(),
  on(AdministrationActions.loadAdministrations, (state) => state),
  on(
    AdministrationActions.loadAdministrationsSuccess,
    (state, action) => state
  ),
  on(
    AdministrationActions.loadAdministrationsFailure,
    (state, action) => state
  ),
  on(AdministrationActions.addNewMemberSuccess, (state, action) => {
    return {
      ...state,
      currentlyOpenCompany: {
        ...state.currentlyOpenCompany,
        members: [...state.currentlyOpenCompany.members, action.data],
      },
    };
  }),
  on(AdministrationActions.addNewMemberFailure, (state, action) => ({
    ...state,
    error: action.error,
  })),
  on(
    AdministrationActions.getAllCompaniesWithParams,
    (state, action) => {
      return {
        ...state,
        companiesParams: {
          filter: action.filter ?? state.companiesParams.filter,
          sort: action.sort ?? state.companiesParams.sort,
        },
        error: null,
      };
    }
  ),
  on(
    AdministrationActions.getAllCompaniesWithParamsSuccess,
    (state, action) => {
      return {
        ...state,
        companies: action.data,
        error: null,
      };
    }
  ),
  on(AdministrationActions.loadRolesSuccess, (state, action)=>({
    ...state,
    roles: action.roles
  })),
  on(AdministrationActions.loadRolesFailure, (state,action)=>({
    ...state,
    error: action.error
  })),
  on(AdministrationActions.getCompanyProjectsSuccess, (state, action)=>({
    ...state,
    currentlyOpenCompany: {...state.currentlyOpenCompany, projects: action.projects, projectsTotalCount: action.total }
  })),
  on(AdministrationActions.getCompanyProjectsFailure, (state,action)=>({
    ...state,
    error: action.error
  })),
  on(
    AdministrationActions.getAllCompaniesWithParamsFailure,
    (state, action) => {
      return {
        ...state,
        companies: null,
        error: action.error,
      };
    }
  ),
  on(AdministrationActions.loadDetailedCompanySuccess, (_state, action) => ({
    ..._state,
    currentlyOpenCompany: {..._state.currentlyOpenCompany, ...action.result},
    error: null,
  })),
  on(AdministrationActions.loadDetailedCompanyFailure, (state, action) => ({
    ...state,
    currentlyOpenCompany: {} as ICompanyDetailed,
    error: action.error,
  })),
  on(AdministrationActions.UploadCompanyImageSuccess, (state, action) => ({
    ...state,
    currentlyOpenCompany: {
      ...state.currentlyOpenCompany,
      image: action.path,
    },
  })),
  on(AdministrationActions.UploadCompanyImageFailure, (state, action) => ({
    ...state,
    error: action.error,
  })),
  on(
    AdministrationActions.loadDisabledCompanyInformationForm,
    (state, action) => ({
      ...state,
      companyInformationForm: disable(
        createFormGroupState<fromCompanyInformationForm.CompanyInformationFormValue>(
          fromCompanyInformationForm.FORM_ID,
          {
            email: state.currentlyOpenCompany.email,
            address: state.currentlyOpenCompany.address,
          }
        )
      ),
    })
  ),
  onNgrxFormsAction(SetValueAction, (state, action) => {
    return {
      ...state,
      companyInformationForm:
        fromCompanyInformationForm.validateCompanyInformationForm(
          state.companyInformationForm
        ),
      currentlyOpenCompany: { ...state.currentlyOpenCompany, name: 'sdlfaj;f' },
    };
  }),
  on(AdministrationActions.editCompanyInformationForm, (state) => {
    {
      return {
        ...state,
        companyInformationForm: enable(state.companyInformationForm),
      };
    }
  }),
  on(AdministrationActions.cancelEditCompanyInformationForm, (state) => {
    return {
      ...state,
      companyInformationForm: disable(state.companyInformationForm),
    };
  }),
  on(
    AdministrationActions.submitCompanyInformationFormFailure,
    (state, action) => {
      return {
        ...state,
        error: action.error,
      };
    }
  ),
  on(
    AdministrationActions.uploadFileSuccess,
    (state, action)=>{
      return{
        ...state,
        failedLines: action.errorLines
      }
    }
  ),
  on(
    AdministrationActions.uploadFileFailure,
    (state, action) => {
      return {
        ...state,
        error: action.error,
      };
    }
  ),
  on(
    AdministrationActions.loadMembersToOpenCompanySuccess,
    (state, action) => ({
      ...state,
      currentlyOpenCompany: {
        ...state.currentlyOpenCompany,
        members: action.result,
      },
    })
  ),
  on(
    AdministrationActions.loadMembersToOpenCompanySuccessFailure,
    (state, action) => ({
      ...state,
      currentlyOpenCompany: {
        ...state.currentlyOpenCompany,
        members: [],
      },
      error: action.error,
    })
  ),
  on(
    AdministrationActions.GetUserDetailsSuccess,
    (state, action) => ({
      ...state,
     currentUserDetails:action.userDetails
    })
  ),
  on(
    AdministrationActions.updateNewCompanyIdSuccess,
    (state, action) => (
      {
        ...state,
        newCompanyId: action.newCompanyId
      })
  )
);
