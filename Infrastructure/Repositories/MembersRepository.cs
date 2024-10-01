using Core.DTOs.TmetricDtos;
using Core.Interfaces;
using Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MembersRepository : IMembersRepository
    {
        public readonly IMongoCollection<SimplifiedMembersDto> _membersCollection;

        public MembersRepository(MongoDbContext context)
        {
            _membersCollection = context.Member;
        }
        public async Task AddMembers(SimplifiedMembersDto Members)
        {
            await _membersCollection.InsertOneAsync(Members);
        }

        public async Task<IEnumerable<SimplifiedMembersDto>> GetAllMembers()
        {
            return await _membersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<string?> GetEmailByUserProfile(int userProfileId)
        {
            SimplifiedMembersDto? simplified = await _membersCollection.Find(data => data.UserProfileId == userProfileId).FirstAsync();
            return simplified.Email;
        }

        public async Task<int?> GetUserProfileIdByEmail(string email)
        {
            SimplifiedMembersDto? simplified = await _membersCollection.Find(data => data.Email == email).FirstAsync();
            return simplified.UserProfileId;
        }
    }
}
