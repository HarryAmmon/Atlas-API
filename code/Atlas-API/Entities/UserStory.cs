using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Atlas_API.Entities
{
    public class UserStory
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("StoryId")]
        [Required]
        public string UserStoryId { get; set; }

        [BsonElement("Title")]
        [Required]
        public string Title { get; set; }

        [BsonElement("StoryPoints")]
        [Range(0, double.MaxValue)]
        public double StoryPoints { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("AcceptanceCriteria")]
        public string AcceptanceCriteria { get; set; }

        [BsonElement("Archived")]
        public bool Archived { get; set; }

        public UserStory(string id, string title, double storyPoints = 0, string description = "", string acceptanceCriteria = "")
        {
            UserStoryId = id;
            Title = title;
            StoryPoints = storyPoints;
            Description = description;
            AcceptanceCriteria = acceptanceCriteria;
        }

        public UserStory() { }

        public override string ToString()
        {
            return $"{Id} {UserStoryId}, {Title}, {StoryPoints}, {Description}, {AcceptanceCriteria}";
        }
    }
}