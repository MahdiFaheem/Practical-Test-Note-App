import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import {
  LoginModel,
  LoginResponseModel,
} from '../../models/authModels/loginModels/loginModel';
import RegisterModel from '../../models/authModels/registerModels/registerModel';
import { AuthService } from '../authService/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthApiService {
  private apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient, private authService: AuthService) {}

  registerUser(registerModel: RegisterModel) {
    return this.http.post(
      `${this.apiUrl}/authentications/register`,
      registerModel
    );
  }

  loginUser(loginModel: LoginModel): Observable<LoginResponseModel> {
    return this.http
      .post<LoginResponseModel>(
        `${this.apiUrl}/authentications/login`,
        loginModel
      )
      .pipe(
        map((data) => {
          if (data) {
            this.authService.setToken(data.accessToken);
          }
          return data;
        })
      );
  }

  logoutUser() {
    this.authService.removeToken();
  }
}
