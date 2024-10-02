using Common.Enums;
using NETCore.Encrypt;

namespace Infrastructure.Data
{
    public class SeedDb
    {
        private readonly SqlDbContext _context;


        #region Builder
        public SeedDb(SqlDbContext context)
        {
            _context = context;
        }
        #endregion

        public async Task ExecSeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

        }
    }
}
