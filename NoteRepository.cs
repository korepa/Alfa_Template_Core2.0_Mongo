using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alfa_Template_Core2_Mongo.Model;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver.GridFS;

namespace Alfa_Template_Core2_Mongo
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteContext _context = null;

        public NoteRepository(IOptions<Settings> settings)
        {
            _context = new NoteContext(settings);
        }

        public async Task AddNote(Note item)
        {
            await _context.Notes.InsertOneAsync(item);
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            try
            {
                return await _context.Notes.Find(_ => true).ToListAsync();
            }
            catch(Exception ex)
            {
                // throw ex if needed
                throw;
            }
        }

        public async Task<Note> GetNote(string id)
        {
            var filter = Builders<Note>.Filter.Eq("Id", id);
            return await _context.Notes
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }

        public async Task<DeleteResult> RemoveAllNotes()
        {
            return await _context.Notes.DeleteManyAsync(
                Builders<Note>.Filter.Empty);
        }

        public async Task<DeleteResult> RemoveNote(string id)
        {
            return await _context.Notes.DeleteOneAsync(
                Builders<Note>.Filter.Eq("Id", id));
        }

        public async Task<UpdateResult> UpdateNote(string id, string body)
        {
            var filter = Builders<Note>.Filter.Eq(s => s.Id, id);
            var update = Builders<Note>.Update
                            .Set(s => s.Body, body)
                            .CurrentDate(s => s.UpdatedOn);

            return await _context.Notes.UpdateOneAsync(filter, update);
        }

        public async Task<ReplaceOneResult> UpdateNote(string id, Note item)
        {
            return await _context.Notes.
                ReplaceOneAsync(n => n.Id.Equals(id),
                    item,
                    new UpdateOptions { IsUpsert = true });
        }

        public async Task<ObjectId> UploadFile(IFormFile file)
        {
            try
            {
                var stream = file.OpenReadStream();
                var filename = file.FileName;
                return await _context.Bucket.UploadFromStreamAsync(filename, stream);
            }
            catch (Exception ex)
            {
                return new ObjectId(ex.ToString());
            }
        }

        public async Task<String> GetFileInfo(string id)
        {
            GridFSFileInfo info = null;
            var objectId = new ObjectId(id);
            try
            {
                using (var stream = await _context.Bucket.OpenDownloadStreamAsync(objectId))
                {
                    info = stream.FileInfo;
                }
                return info.Filename;
            }
            catch (Exception)
            {
                return "Not Found";
            }
        }
    }
}
