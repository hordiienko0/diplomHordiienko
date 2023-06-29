import { createReducer, on } from '@ngrx/store';
import { UserRole } from 'src/app/modules/auth/resources/models/userRole';
import * as fromAuthActions from '../actions/auth.actions';

export const authFeatureKey = 'auth';

export interface User {
  id: number,
  role: UserRole
}

export interface State {
  user: User | null;
  askToChangeDefaultPassword: boolean;
  error: any;
}

export const initialState: State = {
  user: null,
  askToChangeDefaultPassword: false,
  error: null,
};

export const reducer = createReducer(
  initialState,

  on(fromAuthActions.loginSuccess, (state, action) => {
    return {
      ...state,
      user: action.user,
      askToChangeDefaultPassword: action.askToChangeDefaultPassword,
      error: null,
    };
  }),
  on(fromAuthActions.loginFailure, (state, action) => {
    return {
      ...state,
      user: null,
      error: action.error,
    };
  }),
  on(fromAuthActions.logout, (state) => {
    return {
      ...state,
      user: null,
      error: null,
    };
  }),
  on(fromAuthActions.refreshAccessTokenSuccess, (state, action) => {
    return {
      ...state,
      user: action.user,
      error: null,
    };
  }),
);
