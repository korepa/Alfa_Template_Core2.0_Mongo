using Alfa_Template_Core2_Mongo.Model;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alfa_Template_Core2_Mongo
{
    public interface INoteRepository
    {
        Task AddNote(Note item);
        Task<Note> GetNote(string id);
        Task<IEnumerable<Note>> GetAllNotes();

        Task<DeleteResult> RemoveNote(string id);
        Task<DeleteResult> RemoveAllNotes();

        Task<UpdateResult> UpdateNote(string id, string body);
        Task<ReplaceOneResult> UpdateNote(string id, Note item);

        // upload file to gridfs
        Task<ObjectId> UploadFile(IFormFile file);
        Task<String> GetFileInfo(string id);
    }
}
