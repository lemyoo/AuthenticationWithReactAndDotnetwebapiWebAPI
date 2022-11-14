using Microsoft.EntityFrameworkCore;
using AuthenticationWithReactAndDotnetwebapiWebAPI.Models;

namespace AuthenticationWithReactAndDotnetwebapiWebAPI.Data
{
    public class UserAPIDbContext:DbContext
    {
        public UserAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
