import { createAction, props } from "@ngrx/store";
import { HttError } from "src/app/modules/error/resources/models/httpError";
import { User } from "../reducers/auth.reducer";

export const login = createAction(
  '[Auth] Login User',
  props<{ email: string; password: string }>()
);

export const loginSuccess = createAction(
  '[Auth] Login User Success',
  props<{ user: User, askToChangeDefaultPassword: boolean }>()
);

export const loginFailure = createAction(
  '[Auth] Login User Failure',
  props<{ error: any }>()
);

export const logout = createAction('[Auth] Logout User');

export const logoutSuccess = createAction('[Auth] Logout User Success');

export const logoutFailure = createAction(
  '[Auth] Logout User Failure',
  props<{ error: any }>()
);

export const refreshAccessToken = createAction('[Auth] Refresh Access Token');

export const refreshAccessTokenSuccess = createAction(
  '[Auth] Refresh Access Token Success',
  props<{ token: string, user: User }>()
);

export const refreshAccessTokenFailure = createAction(
  '[Auth] Refresh Access Token Failure',
  props<{ error: any }>()
);

export const refreshTokensIfNeeded = createAction(
  '[Auth] Refresh Tokens if needed',
  props<{requiredLogin: boolean}>()
)

export const changeDefaultPassword = createAction(
  '[Auth] Change Default Password',
  props<{ newPassword: string }>()
);

export const changeDefaultPasswordSuccess = createAction(
  '[Auth] Change Default Password Success',
);

export const changeDefaultPasswordFailure = createAction(
  '[Auth] Change Default Password Failure',
  props<{ error: any }>()
);

export const forgotPassword = createAction(
  '[Auth] Forgot Password',
  props<{ email: string }>()
);
export const forgotPasswordSuccess = createAction(
  '[Auth] Forgot Password Success'
);
export const forgotPasswordFailure = createAction(
  '[Auth] Forgot Password Failure',
  props<{ error: HttError }>()
);

export const keepDefaultPassword = createAction('[Auth] Keep Default Password');

