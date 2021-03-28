using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Atlas_API.Entities
{
    public class ColumnGroup
    {
        [BsonElement("_id")]
        public string GroupId { get; set; }

        [BsonElement("Title")]
        [Required]
        public string GroupTitle { get; set; }

        [BsonElement("Limits")]
        [Required]
        [Range(0, int.MaxValue)]
        public int Limits { get; set; }

        [BsonElement("ExitCriteria")]
        public string ExitCriteria { get; set; }
    }
}