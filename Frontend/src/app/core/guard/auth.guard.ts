import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthService } from '../services/authService/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService) {}

  canActivate(): boolean {
    const token = this.authService.getAccessToken();
    if (token) {
      if (!this.authService.tokenExpired(token)) return true;
    } else {
      this.authService.removeToken();
      location.assign('/');
      return false;
    }
  }
}
