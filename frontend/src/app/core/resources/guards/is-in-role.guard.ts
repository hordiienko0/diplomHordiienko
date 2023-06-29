import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import {select, Store} from "@ngrx/store";
import {AppState} from "../../../store";
import {selectUserRole} from "../../../store/selectors/auth.selectors";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class IsInRoleGuard implements CanActivate {

  constructor(private store : Store<AppState>) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.store.pipe(
      select(selectUserRole),
      map(role => route.data['roles']?.includes(role))
    );
  }

}
