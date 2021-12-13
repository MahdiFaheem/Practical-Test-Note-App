import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Note } from 'src/app/core/models/noteModels/Note';
import { NoteTypeEnum } from 'src/app/core/enum/NoteTypeEnum';
import { FilterOptionEnum } from 'src/app/core/enum/FilterOptionEnum';
import { compareDate } from 'src/helpers/DateHelper';
import { NoteApiService } from 'src/app/core/services/apiService/note-api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-note-list',
  templateUrl: './note-list.component.html',
  styleUrls: ['./note-list.component.scss'],
})
export class NoteListComponent implements OnInit {
  notes: Note[] = [];
  filteredNotes: Note[];
  noteTypeEnum = NoteTypeEnum;
  deleteId: string | null = null;
  filterTypes = Object.keys(FilterOptionEnum).map((o) => ({
    value: FilterOptionEnum[o],
    view: o.replace(/_/g, ' '),
  }));
  filterBy: number = 0;

  constructor(private apiService: NoteApiService, private router: Router) {}

  ngOnInit(): void {
    this.getNotes();
  }

  getNotes(): void {
    this.apiService.getNotes().subscribe((data: Note[]) => {
      this.notes = this.filteredNotes = data;
    });
  }

  delete(noteId): void {
    this.apiService.deleteNote(noteId).subscribe(
      (data) => {
        this.deleteId = null;
        alert('Note deleted successfully.');
        this.getNotes();
      },
      (err) => {
        alert(err.error.message);
      }
    );
  }

  onEditClick = (id: string) => {
    location.assign(`/notes/${id}`);
  };

  onFilterChange = (val: number) => {
    this.filterBy = val;
    this.filteredNotes = this.notes.filter((note) =>
      val === FilterOptionEnum.All_Notes
        ? true
        : note.noteDate && compareDate(note.noteDate, val)
    );
  };

  onCompleteChange = (id: string) => {
    const data = this.notes.find((n) => n.id === id);
    if (!data) return;
    data.isComplete = !data.isComplete;
    this.apiService.putNote(id, data).subscribe(
      (data) => {},
      (err) => {
        alert(err.error.message);
      }
    );
  };
}
