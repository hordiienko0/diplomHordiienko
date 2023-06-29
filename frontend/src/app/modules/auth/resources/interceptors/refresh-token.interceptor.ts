import { Injectable } from "@angular/core";
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { BehaviorSubject, filter, Observable, ObservableInput, take, throwError } from "rxjs";
import { catchError, switchMap } from "rxjs/operators";
import { TokenService } from "../services/token.service";
import { Store } from "@ngrx/store";
import { AppState } from "../../../../store";
import * as fromAuthActions from "../../../../store/actions/auth.actions";

@Injectable()
export class RefreshTokenInterceptor implements HttpInterceptor {

  constructor(public tokenService: TokenService, private store: Store<AppState>) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          return this.handleUnauthorized(request, next);
        } else {
          return throwError(error);
        }
      }));
  }

  private isRefreshing = false;
  private refreshTokenSubject = new BehaviorSubject<string | null>(null);

  private handleUnauthorized(request: HttpRequest<any>, next: HttpHandler): ObservableInput<any> {

    if (this.isRefreshing) {
      return this.refreshTokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(token => {
          this.isRefreshing = false;
          return next.handle(this.addToken(request, token!));
        }
        )
      );
    }

    this.isRefreshing = true;
    this.store.dispatch(fromAuthActions.refreshTokensIfNeeded({ requiredLogin: true }))

    const token = this.tokenService.getAccessToken();
    this.refreshTokenSubject.next(token);
    request = this.addToken(request, token);

    return Promise.resolve(request);
  }

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        'Authorization': `Bearer ${token}`
      }
    });
  }
}
