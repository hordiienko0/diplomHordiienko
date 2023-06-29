import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService } from 'src/app/core/resources/services/api.service';
import { Observable } from "rxjs";
import { LoginDto } from '../dtos/login.dto';
import { RefreshTokenDto } from '../dtos/refresh-token.dto';

@Injectable({
  providedIn: "root",
})
export class AuthService extends ApiService {

  constructor(http: HttpClient) {
    super(http);
  }

  login(email: string, password: string): Observable<LoginDto | null> {
    return this.post<LoginDto>('/auth/login', { email, password });
  }

  logout(): Observable<{} | null> {
    return this.post('/auth/logout', {});
  }

  refreshToken(accessToken: string, refreshToken: string): Observable<RefreshTokenDto | null> {
    return this.post<RefreshTokenDto>('/auth/refresh-token', { accessToken, refreshToken });
  }

  keepDefaultPassword(): Observable<{} | null> {
    return this.post('/auth/keep-default-password', {});
  }

  changeDefaultPassword(newPassword: string): Observable<{} | null> {
    return this.post('/auth/change-default-password', { newPassword });
  }

  changePassword(currentPassword: string, newPassword: string): Observable<{} | null> {
    return this.post('/auth/change-password', { currentPassword, newPassword });
  }

  forgotPassword(email: string) {
    return this.post('/auth/forgot-password', { email });
  }
}
