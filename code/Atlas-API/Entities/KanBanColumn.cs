using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Atlas_API.Entities
{
    public class KanBanColumn : BaseColumn
    {
        [BsonElement("ColumnGroup_Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string GroupId { get; set; }
    }
}