import { Injectable, Injector } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import { Router } from "@angular/router";
import * as fromAuthActions from "../../../../store/actions/auth.actions";
import { Store } from "@ngrx/store";
import { AppState } from "../../../../store";
import { of, take } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { AuthEffects } from "../../../../store/effects/auth.effects";
import { UserRole } from '../models/userRole';

@Injectable({
  providedIn: 'root',
})
export class TokenService {

  constructor(
    private jwtHelper: JwtHelperService,
    private router: Router,
    private store: Store<AppState>,
    private injector: Injector,
  ) {
  }

  // here
  setTokens(access_token: string, refresh_token: string, refresh_token_expires_at: Date) {
    localStorage.setItem('access_token', access_token);
    localStorage.setItem('refresh_token', refresh_token);
    localStorage.setItem('refresh_token_expires_at', refresh_token_expires_at.toDateString());
  }

  // here
  setAccessToken(access_token: string) {
    localStorage.setItem('access_token', access_token);
  }

  // here 
  removeTokens() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('refresh_token_expires_at');
  }

  // here
  getAccessToken(): string {
    const token = this.getAccessTokenOrNull();

    if (!token) {
      throw new Error('No access token found');
    }

    return token;
  }

  // here
  getAccessTokenOrNull(): string | null {
    return localStorage.getItem('access_token');
  }

  // here
  getAccessTokenData(): { id: number, role: UserRole, expires: Date } {
    const token = this.getAccessToken();
    const tokenData = this.jwtHelper.decodeToken(token);

    const userRoleKeyEnum = tokenData.role as keyof typeof UserRole;    
    const userRole = UserRole[userRoleKeyEnum];

    if (!tokenData.id || userRole==undefined || !tokenData.exp) {
      throw new Error('A value is missing in the access token payload');
    }
    return {
      id: tokenData.id,
      role: userRole,
      expires: new Date(tokenData.exp * 1000),
    };
  }

  // here
  getRefreshToken(): { token: string, expires: Date } | null {
    const token = localStorage.getItem('refresh_token');
    const expires = localStorage.getItem('refresh_token_expires_at');

    if (!token || !expires) {
      return null;
    }

    return { token: token, expires: new Date(expires) };
  }

  // here
  isAccessTokenExpired(): boolean {
    const token = this.getAccessTokenOrNull();
    return token !== null && this.jwtHelper.isTokenExpired(token);
  }

  // here
  isRefreshTokenExpired(): boolean {
    const token = this.getRefreshToken();
    return token === null || token?.expires < new Date();
  }
}
