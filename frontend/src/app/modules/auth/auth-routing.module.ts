import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthLoginFormComponent } from "./auth-login-form/auth-login-form.component";
import {
  AuthChangeDefaultPasswordFormComponent
} from "./auth-change-default-password-form/auth-change-default-password-form.component";
import { AuthChangePasswordFormComponent } from "./auth-change-password-form/auth-change-password-form.component";
import { ForgotPasswordComponent } from './auth-forgot-password/auth-forgot-password';
import { CanChangeDefaultPasswordGuard } from "./resources/guards/can-change-default-password.guard";
import { NotLoggedInComponent } from "../../shared/components/guards/not-logged-in/not-logged-in.component";
import { LoggedInComponent } from "../../shared/components/guards/logged-in/logged-in.component";
import {IsNotLoggedInGuard} from "../../core/resources/guards/is-not-logged-in.guard";
import {IsLoggedInGuard} from "../../core/resources/guards/is-logged-in.guard";

const routes: Routes = [
  {path: 'login', component: AuthLoginFormComponent, canActivate: [IsNotLoggedInGuard]},
  {path: 'forgot-password', component: ForgotPasswordComponent, canActivate: [IsNotLoggedInGuard]},
  {path: 'change-default-password', component: AuthChangeDefaultPasswordFormComponent, canActivate: [IsLoggedInGuard, CanChangeDefaultPasswordGuard]},
  {path: 'change-password', component: AuthChangePasswordFormComponent, canActivate: [IsLoggedInGuard, CanChangeDefaultPasswordGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class AuthRoutingModule {
}
