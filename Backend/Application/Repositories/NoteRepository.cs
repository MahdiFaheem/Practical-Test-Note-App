using Application.Services.JsonFileService;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface INoteRepository
    {
        Task<Note> Create(Note note);
        Task<List<Note>> GetAll(string userId);
        Task<Note> GetById(string userId, string noteId);
        Task<bool> Update(string noteId, Note note);
        Task<bool> Delete(string userId, string noteId);
    }

    public class NoteRepository : INoteRepository
    {
        private readonly IJsonFileService _jsonFileService;

        public NoteRepository(IJsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        public async Task<Note> Create(Note note)
        {
            var allNotes = await GetAll(note.UserId);
            allNotes.Add(note);
            await _jsonFileService.WriteJsonFile($"../../Data/Notes/{note.UserId}.json", allNotes);
            return note;
        }

        public async Task<bool> Delete(string userId, string noteId)
        {
            var allNotes = await GetAll(userId);
            var foundNote = allNotes.FirstOrDefault(n => n.Id.Equals(noteId));

            if (foundNote == null)
            {
                return false;
            }

            allNotes.Remove(foundNote);
            await _jsonFileService.WriteJsonFile($"../../Data/Notes/{userId}.json", allNotes);

            return true;
        }

        public async Task<List<Note>> GetAll(string userId)
        {
            return await _jsonFileService.ReadJsonFile<List<Note>>($"../../Data/Notes/{userId}.json");
        }

        public async Task<Note> GetById(string userId, string noteId)
        {
            var notes = await GetAll(userId);
            return notes.FirstOrDefault(note => note.Id.Equals(noteId));
        }

        public async Task<bool> Update(string noteId, Note note)
        {
            var allNotes = await GetAll(note.UserId);
            var foundNote = allNotes.FindIndex(n => n.Id.Equals(noteId));

            if (foundNote < 0)
            { 
                return false;
            }

            allNotes[foundNote] = note;
            await _jsonFileService.WriteJsonFile($"../../Data/Notes/{note.UserId}.json", allNotes);

            return true;
        }
    }
}
