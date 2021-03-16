using System;
using System.Collections.Generic;
using Atlas_API.Entities;
using Azure.Security.KeyVault.Secrets;
using MongoDB.Bson;
using MongoDB.Driver;
using Atlas_API.Services;

namespace Atlas_API.Repositories
{
    public class UserStoryRepository : IUserStoryRepository
    {
        private readonly ICollectionRetriever<UserStory> _collection;
        public UserStoryRepository(ICollectionRetriever<UserStory> collection)
        {
            _collection = collection;
        }

        public IEnumerable<UserStory> GetAll()
        {
            var collection = _collection.GetCollection("Atlas", "Atlas");
            var allUsers = collection.Find(_ => true).ToList();

            return allUsers;
        }
    }
}