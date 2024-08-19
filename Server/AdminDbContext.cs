using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.DataModels;

namespace Server
{
    internal class AdminDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EnglishWords> EnglishWords { get; set; }
        public DbSet<UkrainianWords> UkrainianWords { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<EnglishWordsUkrainianWords> EnglishWordsUkrainianWords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnglishWords>(entity =>
            {
                entity.HasMany(e => e.Progress).WithOne(e => e.EnglishWords).OnDelete(DeleteBehavior.Cascade);
            });
        }
        public AdminDbContext(DbContextOptions<AdminDbContext> options)
            :base(options)
        {
            //Database.EnsureCreated();
            //Database.EnsureDeleted();
        }
    }
}
