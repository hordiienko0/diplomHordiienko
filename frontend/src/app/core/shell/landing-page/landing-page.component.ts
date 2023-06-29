import {Component, OnInit} from '@angular/core';
import {select, Store} from "@ngrx/store";
import {selectIsLoggedIn, selectUserIsAdmin} from "../../../store/selectors/auth.selectors";
import {AppState} from "../../../store";
import {Observable} from "rxjs";
import {closeMenu} from "../../../store/actions/menu.actions";
import {logout} from "../../../store/actions/auth.actions";

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {

  isLoggedIn$? : Observable<boolean>
  isAdmin$? : Observable<boolean>

  constructor(private store: Store<AppState>) {
    this.isLoggedIn$ = this.store.pipe(select(selectIsLoggedIn));
    this.isAdmin$ = this.store.pipe(select(selectUserIsAdmin));

    this.store.dispatch(closeMenu());
  }

  ngOnInit(): void {
  }


}
