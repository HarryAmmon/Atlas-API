using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Atlas_API.Entities
{
    public class BaseColumn
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ColumnId { get; set; }

        [BsonElement("Title")]
        [Required]
        public string Title { get; set; }

        [BsonElement("Visible")]
        [BsonDefaultValue(true)]
        public bool Visible { get; set; }

        [BsonElement("UserStories_Id")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> UserStoriesId { get; set; }
    }
}