import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/authService/auth.service';

@Injectable()
export class JwtInterceptorInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.authService.getAccessToken();
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
    }
    return next.handle(request).pipe(
      catchError((res) => {
        if (res instanceof HttpErrorResponse && res.status === 401) {
          this.authService.removeToken();
          location.assign('/');
        }
        return throwError(res);
      })
    );
  }

  private addToken(request: HttpRequest<any>, token: any): HttpRequest<any> {
    request.clone({
      setHeaders: {
        Authorization: `bearer ${token}`,
      },
    });
    return request;
  }
}
