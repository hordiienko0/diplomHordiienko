import {Component} from '@angular/core';
import {select, Store} from '@ngrx/store';
import {AppState} from './store';
import {selectIsLoggedIn, selectUserIsAdmin} from "./store/selectors/auth.selectors";
import {TokenService} from "./modules/auth/resources/services/token.service";
import {Observable} from "rxjs";
import { ThisReceiver } from '@angular/compiler';
import { refreshTokensIfNeeded } from './store/actions/auth.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'frontend';

  constructor(private store: Store<AppState>) {
    this.store.dispatch(refreshTokensIfNeeded({requiredLogin: true}))
  }

}
