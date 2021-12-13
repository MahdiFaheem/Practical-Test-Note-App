import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginModel } from 'src/app/core/models/authModels/loginModels/loginModel';
import { AuthApiService } from 'src/app/core/services/apiService/auth-api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  loginModel: LoginModel = new LoginModel();
  submitted: boolean = false;

  constructor(
    private formGroup: FormBuilder,
    private apiService: AuthApiService
  ) {}

  ngOnInit(): void {
    this.initializeFrom();
  }

  initializeFrom(): void {
    this.form = this.formGroup.group({
      password: [this.loginModel.password, [Validators.required]],
      email: [this.loginModel.email, [Validators.required, Validators.email]],
    });
  }

  get f() {
    return this.form.controls;
  }

  onLogin(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.loginModel = this.form.value as LoginModel;
    this.apiService.loginUser(this.loginModel).subscribe(
      (data) => {
        location.assign('/notes');
      },
      (err) => {
        alert(err.error.message);
      }
    );
  }
}
