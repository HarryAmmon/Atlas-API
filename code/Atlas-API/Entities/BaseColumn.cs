using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

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
        [Required]
        [BsonDefaultValue(false)]
        public bool Visible { get; set; }

        [BsonElement("UserStories_Id")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string[] UserStoriesId { get; set; }
    }
}