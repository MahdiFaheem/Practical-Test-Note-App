import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './authPages/login/login.component';
import { RegisterComponent } from './authPages/register/register.component';
import { NoteListComponent } from './notesPages/note-list/note-list.component';
import { CreateNoteComponent } from './notesPages/create-note/create-note.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthService } from './core/services/authService/auth.service';
import { JwtInterceptorInterceptor } from './core/interceptor/jwt-interceptor.interceptor';
import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NoteListComponent,
    CreateNoteComponent,
    NavbarComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  bootstrap: [AppComponent],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptorInterceptor,
      multi: true,
      deps: [AuthService],
    },
  ],
})
export class AppModule {}
