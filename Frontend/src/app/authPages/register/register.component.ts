import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import RegisterModel from 'src/app/core/models/authModels/registerModels/registerModel';
import { AuthApiService } from 'src/app/core/services/apiService/auth-api.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  form: FormGroup;
  registerModel: RegisterModel = new RegisterModel();
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
      name: [this.registerModel.name, [Validators.required]],
      password: [this.registerModel.password, [Validators.required]],
      email: [
        this.registerModel.email,
        [Validators.required, Validators.email],
      ],
      dateOfBirth: [this.registerModel.dateOfBirth, [Validators.required]],
    });
  }

  get f() {
    return this.form.controls;
  }

  onRegister(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.registerModel = this.form.value as RegisterModel;
    this.apiService.registerUser(this.registerModel).subscribe(
      (data) => {
        alert('Registration Successful');

        location.assign('/');
      },
      (err) => {
        alert('Registration Failed');
      }
    );
  }
}
