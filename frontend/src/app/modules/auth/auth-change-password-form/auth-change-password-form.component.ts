import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Store } from "@ngrx/store";
import { AppState } from "../../../store";
import * as fromAuthActions from "../../../store/actions/auth.actions";

@Component({
  selector: 'auth-change-password-form',
  templateUrl: './auth-change-password-form.component.html',
  styleUrls: ['./auth-change-password-form.component.scss']
})
export class AuthChangePasswordFormComponent {

  passwordMatchingValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    return this.password?.value === this.confirmPassword?.value ? null : { notMatched: true };
  };

  password = new FormControl<string>('', [Validators.required, Validators.minLength(5)]);
  confirmPassword = new FormControl<string>('', [Validators.required, Validators.minLength(5)]);

  form = new FormGroup({
    password: this.password,
    confirmPassword: this.confirmPassword,
  }, { validators: this.passwordMatchingValidator });

  constructor(private store: Store<AppState>) {
  }

  submit() {
    this.form.markAllAsTouched();

    if (!this.form.valid) {
      return;
    }

    this.store.dispatch(fromAuthActions.changeDefaultPassword({
      newPassword: this.password.value!,
    }));
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
