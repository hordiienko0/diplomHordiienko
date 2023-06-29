import { Component } from '@angular/core';
import { select, Store } from "@ngrx/store";
import * as authSelectors from "../../../../store/selectors/auth.selectors";
import { AppState } from "../../../../store";
import { TokenService } from "../../../../modules/auth/resources/services/token.service";
import { refreshTokensIfNeeded } from 'src/app/store/actions/auth.actions';

@Component({
  selector: 'is-not-logged-in',
  template: `
    <div *ngIf="!(isLoggedIn$ | async)">
      <router-outlet></router-outlet>
    </div>`,
})
export class NotLoggedInComponent {

  isLoggedIn$ = this.store.pipe(select(authSelectors.selectIsLoggedIn));

  constructor(private store: Store<AppState>) {
    this.store.dispatch(refreshTokensIfNeeded({requiredLogin: true}))
  }

}
