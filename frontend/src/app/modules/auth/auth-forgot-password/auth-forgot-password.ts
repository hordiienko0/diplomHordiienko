import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store';
import { forgotPassword } from 'src/app/store/actions/auth.actions';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './auth-forgot-password.html',
  styleUrls: ['./auth-forgot-password.scss']
})
export class ForgotPasswordComponent {

  email = new FormControl<string>('', [Validators.required, Validators.email]);

  form = new FormGroup({
    email: this.email,
  });

  isSubmitted = false;

  constructor(private store: Store<AppState>) {
  }

  submit() {
    console.log(this.form)
    if (!this.form.valid) {
      this.isSubmitted = false
      return;
    }
    this.isSubmitted = true

    this.store.dispatch(forgotPassword({
      email: this.email.value!
    }));
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

}
