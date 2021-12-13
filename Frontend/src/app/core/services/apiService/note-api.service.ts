import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Note } from '../../models/noteModels/Note';

@Injectable({
  providedIn: 'root',
})
export class NoteApiService {
  private apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getNotes(): Observable<Note[]> {
    return this.http.get<Note[]>(`${this.apiUrl}/notes`);
  }

  getNote(id: string): Observable<Note> {
    return this.http.get<Note>(`${this.apiUrl}/notes/${id}`);
  }

  postNote(noteModel: Note) {
    return this.http.post(`${this.apiUrl}/notes`, noteModel);
  }

  putNote(id: string, noteModel: Note) {
    return this.http.put(`${this.apiUrl}/notes/${id}`, noteModel);
  }

  deleteNote(id: string) {
    return this.http.delete(`${this.apiUrl}/notes/${id}`);
  }
}
