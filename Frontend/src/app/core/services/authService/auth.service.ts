import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor() {}

  setToken(accessToken) {
    localStorage.setItem('accessToken', accessToken);
  }

  getAccessToken() {
    return localStorage.getItem('accessToken');
  }

  removeToken() {
    localStorage.removeItem('accessToken');
  }

  public tokenExpired(token: string) {
    const expiry = JSON.parse(atob(token.split('.')[1])).exp;
    return Math.floor(new Date().getTime() / 1000) >= expiry;
  }

  isUserLoggedIn() {
    return this.getAccessToken() && !this.tokenExpired(this.getAccessToken());
  }
}
