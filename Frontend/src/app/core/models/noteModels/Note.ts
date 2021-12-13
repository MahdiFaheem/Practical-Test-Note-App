export class Note {
  id: string;
  noteType: number = 0;
  noteMessage: string = '';
  noteDate?: string = null;
  isComplete?: boolean = null;
  userId: string;
}
