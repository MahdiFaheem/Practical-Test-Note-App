import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NoteTypeEnum } from 'src/app/core/enum/NoteTypeEnum';
import { Note } from 'src/app/core/models/noteModels/Note';
import { NoteApiService } from 'src/app/core/services/apiService/note-api.service';

@Component({
  selector: 'app-create-note',
  templateUrl: './create-note.component.html',
  styleUrls: ['./create-note.component.scss'],
})
export class CreateNoteComponent implements OnInit {
  form: FormGroup;
  noteModel: Note = new Note();
  noteTypes: { value: number; view: string }[] = Object.keys(NoteTypeEnum).map(
    (n) => ({
      value: +n,
      view: NoteTypeEnum[n],
    })
  );
  id: string;
  selectedNoteType: number = 0;
  dateRequired: boolean = false;
  submitted: boolean = false;

  constructor(
    private formGroup: FormBuilder,
    private apiService: NoteApiService,
    private activeRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initializeFrom();
    this.getEditValue();
  }

  initializeFrom(): void {
    this.form = this.formGroup.group({
      noteMessage: [this.noteModel.noteMessage, Validators.required],
      noteType: [this.noteModel.noteType],
      noteDate: [this.noteModel.noteDate],
      isComplete: [this.noteModel.isComplete],
    });
  }

  get f() {
    return this.form.controls;
  }

  getEditValue() {
    this.activeRoute.paramMap.subscribe((prmas) => {
      this.id = prmas.get('id').toString();
      if (this.id) {
        this.getNote(this.id);
      }
    });
  }

  onNoteTypeChange(type: number) {
    this.selectedNoteType = type;
    if (type === 0 || type === 3) {
      this.form.controls['noteDate'].clearValidators();
    } else {
      this.form.controls['noteDate'].setValidators([Validators.required]);
    }
  }

  getNote(id: string): void {
    this.apiService.getNote(id).subscribe((data: Note) => {
      this.form.controls['noteMessage'].setValue(data.noteMessage);
      this.form.controls['noteType'].setValue(data.noteType);
      this.form.controls['noteDate'].setValue(data.noteDate);
      this.form.controls['isComplete'].setValue(data.isComplete);

      this.selectedNoteType = data.noteType;
    });
  }

  onSave(): void {
    this.submitted = true;
    this.noteModel = this.form.value as Note;
    this.noteModel.noteType = this.selectedNoteType;

    if (this.noteModel.noteType == 2 && this.noteModel.isComplete === null) {
      this.noteModel.isComplete = false;
    }

    if (this.form.invalid) {
      return;
    }

    if (!this.id) {
      this.apiService.postNote(this.noteModel).subscribe(
        (data) => {
          alert('Successfully Saved');
          location.assign('/notes');
        },
        (err) => {
          alert(err.error.message);
        }
      );
    } else {
      this.apiService.putNote(this.id, this.noteModel).subscribe(
        (data) => {
          alert('Successfully Saved');
          location.assign('/notes');
        },
        (err) => {
          alert(err.error.message);
        }
      );
    }
  }
}
