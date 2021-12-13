using Application.DTOs.EntityDTOs;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Managers
{
    public interface INoteManager
    {
        Task<Note> CreateNote(NoteDTO noteDTO);
        Task<List<Note>> GetAllNotes(string userId);
        Task<Note> GetNote(string userId, string noteId);
        Task<bool> UpdateNote(string noteId, NoteDTO noteDTO);
        Task<bool> DeleteNote(string userId, string noteId);
    }

    public class NoteManager : INoteManager
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NoteManager(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<Note> CreateNote(NoteDTO noteDTO)
        {
            var note = _mapper.Map<Note>(noteDTO);
            note.Id = Guid.NewGuid().ToString();
            return await _noteRepository.Create(note);
        }

        public async Task<bool> DeleteNote(string userId, string noteId)
        {
            return await _noteRepository.Delete(userId, noteId);
        }

        public async Task<List<Note>> GetAllNotes(string userId)
        {
            return await _noteRepository.GetAll(userId);
        }

        public async Task<Note> GetNote(string userId, string noteId)
        {
            return await _noteRepository.GetById(userId, noteId);
        }

        public async Task<bool> UpdateNote(string noteId, NoteDTO noteDTO)
        {
            var note = await _noteRepository.GetById(noteDTO.UserId, noteId);

            if (note == null)
            {
                return false;
            }

            note = _mapper.Map(noteDTO, note);

            return await _noteRepository.Update(noteId, note);
        }
    }
}
