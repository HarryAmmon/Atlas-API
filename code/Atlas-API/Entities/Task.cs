using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Atlas_API.Entities
{
    public class Task
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }

        [BsonElement("Title")]
        [Required]
        public string Title { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Completed")]
        public bool Completed { get; set; }
    }
}