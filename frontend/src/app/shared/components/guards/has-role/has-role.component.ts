import { Component } from '@angular/core';
import { select, Store } from "@ngrx/store";
import * as authSelectors from "../../../../store/selectors/auth.selectors";
import { AppState } from "../../../../store";
import { TokenService } from "../../../../modules/auth/resources/services/token.service";
import { combineLatest, Observable } from "rxjs";
import { ActivatedRoute } from "@angular/router";
import { map } from "rxjs/operators";
import { refreshTokensIfNeeded } from 'src/app/store/actions/auth.actions';

@Component({
  selector: 'has-role',
  template: `
    <div *ngIf="hasRole$ | async">
      <router-outlet></router-outlet>
    </div>`,
})
export class HasRoleComponent {

  hasRole$: Observable<boolean> = combineLatest(this.store.pipe(select(authSelectors.selectUserRole)), this.route.data).pipe(
    map(([role, requiredRole]) => role === requiredRole['requiredRole'])
  );

  constructor(private store: Store<AppState>, private route: ActivatedRoute) {
    this.store.dispatch(refreshTokensIfNeeded({requiredLogin: true}))
  }

}
