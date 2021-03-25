using NUnit.Framework;
using Atlas_API.Entities;

namespace Atlas_API.Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Must_Have_ID()
        {
            // Arrange
            string id = "myID";

            // Act
            var result = new UserStory(id, "title");

            // Arrange
            Assert.AreEqual(result.UserStoryId, id);
        }

        [Test]
        public void Must_Have_Title()
        {
            // Arrange
            string title = "a title";

            // Act
            var result = new UserStory("1", title);

            // Arrange
            Assert.AreEqual(result.Title, title);
        }

        [Test]
        public void Story_Points_Optional_Value_Is_Zero()
        {
            // Act
            var result = new UserStory("123", "title");

            // Arrange
            Assert.AreEqual(result.StoryPoints, 0);
        }

        [Test]
        public void Description_Optional_Value_Is_WhiteSpace()
        {
            // Act
            var result = new UserStory("123", "title");

            // Arrange
            Assert.That(string.IsNullOrWhiteSpace(result.Description));
        }

        [Test]
        public void AcceptanceCriteria_Optional_Value_Is_WhiteSpace()
        {
            // Act
            var result = new UserStory("123", "title");

            // Arrange
            Assert.That(string.IsNullOrWhiteSpace(result.AcceptanceCriteria));
        }
    }
}