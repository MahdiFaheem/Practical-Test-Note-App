import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthApiService } from '../core/services/apiService/auth-api.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  constructor(private authApiService: AuthApiService, private router: Router) {}

  ngOnInit(): void {}

  onTitleClick() {
    location.assign(`/notes`);
  }

  logout() {
    this.authApiService.logoutUser();
    location.assign('/');
  }
}
