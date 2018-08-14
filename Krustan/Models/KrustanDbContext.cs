using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Krustan.Models
{
    public class KrustanDbContext : IdentityDbContext
    {
        public KrustanDbContext(DbContextOptions<KrustanDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        
    }
}
