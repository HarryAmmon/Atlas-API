using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Atlas_API.Entities
{
    public class ColumnGroup
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GroupId { get; set; }

        [BsonElement("Title")]
        [Required]
        public string GroupTitle { get; set; }

        [BsonElement("Limits")]
        [Required]
        public int Limits { get; set; }

        [BsonElement("ExitCriteria")]
        [Required]
        public string ExitCriteria { get; set; }
    }
}