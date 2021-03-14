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
            string x = "x";
        
            // Act
            UserStory result = new UserStory(x, "title");
            
            // Arrange
            Assert.AreEqual(result.Id, x);
    
        }

        [Test]
        public void Test2()
        {
            Assert.Pass();

        }
    }
}