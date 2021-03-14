namespace Atlas_API.Entities
{
    public class UserStory
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double? StoryPoints { get; set; }
        public string? Description { get; set; }
        public string? AcceptanceCriteria { get; set; }

        public UserStory(string id, string title, double? storyPoints, string? description, string? acceptanceCriteria)
        {
            Id = id;
            Title = title;
            StoryPoints = storyPoints;
            Description = description;
            AcceptanceCriteria = acceptanceCriteria;
        }
    }
}