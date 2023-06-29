import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from "@ngrx/store";
import { AppState } from "../../../store";
import { login } from "../../../store/actions/auth.actions";

@Component({
  selector: 'auth-login-form',
  templateUrl: './auth-login-form.component.html',
  styleUrls: ['./auth-login-form.component.scss']
})
export class AuthLoginFormComponent {
  email = new FormControl<string>('', [Validators.required, Validators.email]);
  password = new FormControl<string>('', [Validators.required, Validators.minLength(5)]);

  form = new FormGroup({
    email: this.email,
    password: this.password,
  });

  constructor(private store: Store<AppState>) {
  }

  submit() {
    this.form.markAllAsTouched();

    if (!this.form.valid) {
      return;
    }

    this.store.dispatch(login({ email: this.email.value!, password: this.password.value! }));
  }

  emailError() {
    if (this.email.hasError('required')) {
      return 'Email is required';
    }
    if (this.email.hasError('email')) {
      return 'Please enter a valid email address';
    }
    return '';
  }

  passwordError() {
    if (this.password.hasError('required')) {
      return 'Password is required';
    }
    if (this.password.hasError('minlength')) {
      return 'Password must be at least 5 characters long';
    }
    return '';
  }

}
