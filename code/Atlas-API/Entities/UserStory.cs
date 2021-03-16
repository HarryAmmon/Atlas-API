using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Atlas_API.Entities
{
    public class UserStory
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("StoryId")]
        public string StoryId { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("StoryPoints")]
        public double StoryPoints { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("AcceptanceCriteria")]
        public string AcceptanceCriteria { get; set; }

        public UserStory(string id, string title, double storyPoints = 0, string description = "", string acceptanceCriteria = "")
        {
            StoryId = id;
            Title = title;
            StoryPoints = storyPoints;
            Description = description;
            AcceptanceCriteria = acceptanceCriteria;
        }
    }
}