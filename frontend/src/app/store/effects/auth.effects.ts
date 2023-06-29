import { Injectable } from '@angular/core';
import { act, Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, switchMap, tap } from 'rxjs/operators';
import { of, take } from 'rxjs';
import * as AuthActions from '../actions/auth.actions';
import { AuthService } from 'src/app/modules/auth/resources/services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import * as fromAuthActions from '../actions/auth.actions';
import { TokenService } from "../../modules/auth/resources/services/token.service";
import { serializeError } from "serialize-error";
import { setMenuLinks } from "../actions/menu.actions";
import { AppState } from "../index";
import { Store } from "@ngrx/store";
import * as RouteActions from '../actions/route.actions';


import * as AdministrationActions from "../../modules/administration/state/administration.actions"
import { NotificationService } from '../../shared/services/notification.service';
import { RouteEffects } from './route.effects';
import { reject, resolve } from 'cypress/types/bluebird';
@Injectable()
export class AuthEffects {

  login$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(fromAuthActions.login),
      mergeMap((action) =>
        this.authService.login(action.email, action.password).pipe(
          map(response => {

            this.tokenService.setTokens(
              response!.accessToken.token,
              response!.refreshToken.token,
              new Date(response!.refreshToken.expires)
            );

            const tokenData = this.tokenService.getAccessTokenData();

            return fromAuthActions.loginSuccess({
              user: {
                ...response!.user,
                role: tokenData.role,
              },
              askToChangeDefaultPassword: response?.askToChangePassword ?? false,
            })
          }),
          catchError((error: any) => of(fromAuthActions.loginFailure(error)))
        )
      )
    );
  });

  resetPassword$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AuthActions.forgotPassword),
      mergeMap((action) =>
        this.authService.forgotPassword(action.email).pipe(
          map(() => AuthActions.forgotPasswordSuccess()),
          catchError((error: HttpErrorResponse) => of(AuthActions.forgotPasswordFailure({ error: error.error })))
        )
      )
    )
  });

  logout$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(fromAuthActions.logout),
      mergeMap((action) => {
        this.notifService.stopService();
        return this.authService.logout().pipe(
          map(response => {
            this.tokenService.removeTokens();
            return fromAuthActions.logoutSuccess();
          }),
          catchError((error: any) => {
            this.tokenService.removeTokens();
            return of(fromAuthActions.logoutFailure(serializeError(error)));
          }),
        );
      }),
      catchError((error: any) => of(fromAuthActions.logoutFailure(serializeError(error))))
    );
  });

  refreshAccessToken$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(fromAuthActions.refreshAccessToken),
      mergeMap((action) =>

        this.authService.refreshToken(this.tokenService.getAccessToken(), this.tokenService.getRefreshToken()!.token).pipe(
          map(response => {

            this.tokenService.setAccessToken(response!.token);
            const tokenData = this.tokenService.getAccessTokenData();
            const expires = response!.expires.toString();

            return fromAuthActions.refreshAccessTokenSuccess({
              token: response!.token,
              user: {
                id: tokenData.id,
                role: tokenData.role,
              },
            })

          }),
          catchError((error: any) => of(fromAuthActions.refreshAccessTokenFailure(error))),
        )),

      catchError((error: any) => of(fromAuthActions.refreshAccessTokenFailure(error))),
    );
  });

  refreshTokensIfNeeded$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(fromAuthActions.refreshTokensIfNeeded),
      map((action) => {
        var tokenData = this.tokenService.getAccessTokenData();
        const isAuth = action.requiredLogin;

          if (!tokenData) {
            if (isAuth) {
              this.tokenService.removeTokens()
              RouteActions.navigate({ commands: ['/'] })
            }
          }
  
          if (!this.tokenService.isAccessTokenExpired()) {
            fromAuthActions.refreshAccessTokenSuccess({
              token: this.tokenService.getRefreshToken()!.token,
              user: {
                id: tokenData.id,
                role: tokenData.role
              }
            })
          }
  
          if (this.tokenService.isRefreshTokenExpired()) {
            fromAuthActions.logout()
            RouteActions.navigate({ commands: ['/login'] })
          }
  
          return fromAuthActions.refreshAccessToken();
      }))})



  changeDefaultPassword$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(fromAuthActions.changeDefaultPassword),
      mergeMap((action) =>

        this.authService.changeDefaultPassword(action.newPassword).pipe(
          map(response => fromAuthActions.changeDefaultPasswordSuccess()),
          catchError((error: any) => of(fromAuthActions.changeDefaultPasswordFailure(error))))
      )
    );
  });

  keepDefaultPassword$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(fromAuthActions.keepDefaultPassword),
      mergeMap((action) =>
        this.authService.keepDefaultPassword())
    )
  }, { dispatch: false }
  );



  loginSucces$ = createEffect(() =>
    this.actions$.pipe(
      ofType(fromAuthActions.loginSuccess),
      tap(() =>
        this.notifService.startService()
      )
    ),
    { dispatch: false });

  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private tokenService: TokenService,
    private notifService: NotificationService
  ) {
  }


}
