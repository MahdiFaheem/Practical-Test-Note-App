import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './authPages/login/login.component';
import { RegisterComponent } from './authPages/register/register.component';
import { NoteListComponent } from './notesPages/note-list/note-list.component';
import { CreateNoteComponent } from './notesPages/create-note/create-note.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'notes/create', component: CreateNoteComponent },
  { path: 'notes/edit/:id', component: CreateNoteComponent },
  { path: 'notes', component: NoteListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
