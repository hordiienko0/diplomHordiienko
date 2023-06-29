import { Component } from '@angular/core';
import { Store } from "@ngrx/store";
import { AppState } from "../../../store";
import * as fromAuthActions from "../../../store/actions/auth.actions";

@Component({
  selector: 'auth-change-default-password-form',
  templateUrl: './auth-change-default-password-form.component.html',
  styleUrls: ['./auth-change-default-password-form.component.scss']
})
export class AuthChangeDefaultPasswordFormComponent {

  constructor(private store: Store<AppState>) {
  }

  skip() {
    this.store.dispatch(fromAuthActions.keepDefaultPassword());
  }

}
