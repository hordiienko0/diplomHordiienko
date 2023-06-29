import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { select, Store } from "@ngrx/store";
import { AppState } from "../../../../store";
import { map } from "rxjs/operators";
import * as fromAuth from "../../../../store/selectors/auth.selectors";

@Injectable({
  providedIn: 'root'
})
export class CanChangeDefaultPasswordGuard implements CanActivate {
  constructor(private store: Store<AppState>) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.store.pipe(
      select(fromAuth.selectAskToChangeDefaultPassword)
    );
  }

}
