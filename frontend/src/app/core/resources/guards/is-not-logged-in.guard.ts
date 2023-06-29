import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import {select, Store} from "@ngrx/store";
import {selectIsLoggedIn} from "../../../store/selectors/auth.selectors";
import {AppState} from "../../../store";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class IsNotLoggedInGuard implements CanActivate {

  constructor(private store : Store<AppState>) {
  }


  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.store.pipe(select(selectIsLoggedIn), map(_ => !_));
  }

}
