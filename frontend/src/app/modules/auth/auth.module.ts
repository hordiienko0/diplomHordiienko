import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StoreModule } from '@ngrx/store';
import * as fromAuth from '../../store/reducers/auth.reducer';
import { EffectsModule } from '@ngrx/effects';
import { JwtModule } from "@auth0/angular-jwt";
import { AuthEffects } from '../../store/effects/auth.effects';
import { AuthService } from "./resources/services/auth.service";
import { AuthPageLayoutComponent } from './auth-page-layout/auth-page-layout.component';
import { AuthLoginFormComponent } from './auth-login-form/auth-login-form.component';
import {
  AuthChangeDefaultPasswordFormComponent
} from "./auth-change-default-password-form/auth-change-default-password-form.component";
import { AuthChangePasswordFormComponent } from "./auth-change-password-form/auth-change-password-form.component";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { ForgotPasswordComponent } from './auth-forgot-password/auth-forgot-password';
import { environment } from "../../../environments/environment";
import { RefreshTokenInterceptor } from "./resources/interceptors/refresh-token.interceptor";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { SharedModule } from "../../shared/shared.module";

@NgModule({
  declarations: [
    AuthPageLayoutComponent,
    AuthLoginFormComponent,
    AuthChangeDefaultPasswordFormComponent,
    AuthChangePasswordFormComponent,
    ForgotPasswordComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    StoreModule.forFeature(
      fromAuth.authFeatureKey,
      fromAuth.reducer
    ),
    EffectsModule.forFeature([AuthEffects]),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('access_token'),
        headerName: 'Authorization',
        authScheme: 'Bearer ',
        allowedDomains: [environment.apiHost],
      }
    }),
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    SharedModule,
  ],
  exports: [
    AuthPageLayoutComponent,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RefreshTokenInterceptor,
      multi: true
    },
    AuthService,
  ],
})
export class AuthModule {
}
