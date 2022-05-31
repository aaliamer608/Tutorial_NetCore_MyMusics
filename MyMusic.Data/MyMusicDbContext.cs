using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core;
using MyMusic.Core.Models.Auth;
using MyMusic.Data.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Data
{
    public class MyMusicDbContext : IdentityDbContext<User, Role, Guid> //DbContext
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }

        public MyMusicDbContext(DbContextOptions<MyMusicDbContext> options)
            : base(options)
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder
                .ApplyConfiguration(new MusicConfig());

            builder
                .ApplyConfiguration(new ArtistConfig());
        }
    }
}
