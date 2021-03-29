using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace Atlas_API.Entities
{
    public class Column
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        [Required]
        public string Title { get; set; }

        [BsonElement("ColumnGroup_Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string ColumnGroupId { get; set; }

        [BsonElement("UserStories_Id")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string[] UserStoriesId { get; set; }
    }
}