using Microsoft.EntityFrameworkCore;
using PartyInvites.Models;

namespace PartyInvites.Data
{
    public class AppDbContext : DbContext

    {

        public AppDbContext(DbContextOptions<AppDbContext> options)

            : base(options)

        { }

        public DbSet<GuestResponse> GuestResponses { get; set; }

    }
}
