using Application.DTOs.EntityDTOs;
using Application.Extensions;
using Application.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class NotesController : BaseController
    {
        private readonly INoteManager _noteManager;

        public NotesController(INoteManager noteManager)
        {
            _noteManager = noteManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteDTO>>> Get()
        {
            return Ok(await _noteManager.GetAllNotes(HttpContext.GetUserId()));
        }

        [HttpGet("{noteId}")]
        public async Task<ActionResult<NoteDTO>> Get(string noteId)
        {
            var result = await _noteManager.GetNote(HttpContext.GetUserId(), noteId);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(NoteDTO noteDTO)
        {
            noteDTO.UserId = HttpContext.GetUserId();
            var result = await _noteManager.CreateNote(noteDTO);

            return (result != null) ? CreatedAtAction(nameof(Get), new { noteId = result.Id }, result)
                : BadRequest(_apiResponseDTO.SetApiResponse("Could not create note."));
        }

        [HttpPut("{nodeId}")]
        public async Task<IActionResult> Put(string nodeId, NoteDTO noteDTO)
        {
            noteDTO.UserId = HttpContext.GetUserId();
            var result = await _noteManager.UpdateNote(nodeId, noteDTO);

            return (result) ? NoContent() : NotFound();
        }

        [HttpDelete("{noteId}")]
        public async Task<ActionResult<NoteDTO>> Delete(string noteId)
        {
            var result = await _noteManager.DeleteNote(HttpContext.GetUserId(), noteId);

            return (result) ? NoContent() : NotFound();
        }
    }
}
