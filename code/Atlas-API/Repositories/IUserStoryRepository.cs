using System.Collections.Generic;
using Atlas_API.Entities;

namespace Atlas_API.Repositories
{
    public interface IUserStoryRepository
    {
        IEnumerable<UserStory> GetAll();
    }
}