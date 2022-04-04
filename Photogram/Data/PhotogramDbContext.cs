using Microsoft.EntityFrameworkCore;
using Photogram.Models;

namespace Photogram
{
    public class PhotogramDbContext :DbContext
    {
        public PhotogramDbContext(DbContextOptions<PhotogramDbContext> options): base(options)
        {
                    
        }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Reactions> Reactions { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
