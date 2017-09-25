﻿using Alfa_Template_Core2_Mongo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alfa_Template_Core2_Mongo.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data")]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [ResponseCache(NoStore =true, Location =ResponseCacheLocation.None)]
        [HttpGet]
        public Task<IEnumerable<Note>> Get()
        {
            return GetNoteInternal();
        }

        private async Task<IEnumerable<Note>> GetNoteInternal()
        {
            return await _noteRepository.GetAllNotes();
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public Task<Note> Get(string id)
        {
            return GetNoteByIdInternal(id);
        }

        private async Task<Note> GetNoteByIdInternal(string id)
        {
            return await _noteRepository.GetNote(id) ?? new Note();
        }

        // POST api/notes
        [HttpPost]
        public void Post([FromBody]string value)
        {
            _noteRepository.AddNote(new Note() { Body = value, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now });
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _noteRepository.UpdateNote(id, value);
        }

        // DELETE api/notes/23243423
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _noteRepository.RemoveNote(id);
        }

        // POST api/notes/uploadFile
        [HttpPost("uploadFile")]
        public async Task<ObjectId> UploadFile(IFormFile file)
        {
            return await _noteRepository.UploadFile(file);
        }

        // POST api/notes/getFileInfo/234244234
        [HttpGet("getFileInfo/{id}")]
        public async Task<String> GetFileInfo(string id)
        {
            return await _noteRepository.GetFileInfo(id);
        }
    }
}
