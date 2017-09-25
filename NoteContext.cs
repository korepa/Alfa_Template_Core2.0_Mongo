using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Alfa_Template_Core2_Mongo
{
    public class NoteContext
    {
        private readonly IMongoDatabase _database = null;
        private readonly GridFSBucket _bucket = null;

        public NoteContext(IOptions<Model.Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
                var gridFSBucketOptions = new GridFSBucketOptions()
                {
                    BucketName = "images",
                    ChunkSizeBytes = 1048576, // 1 mb
                };
                _bucket = new GridFSBucket(_database, gridFSBucketOptions);
            }
        }

        public IMongoCollection<Model.Note> Notes
        {
            get
            {
                return _database.GetCollection<Model.Note>("Note");
            }
        }

        public GridFSBucket Bucket
        {
            get
            {
                return _bucket;
            }
        }
    }
}
