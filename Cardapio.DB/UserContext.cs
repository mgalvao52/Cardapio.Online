using Cardapio.DB.Entiites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cardapio.DB
{
    public class UserContext : IdentityDbContext<UserEntity, IdentityRole<string>, string>
    {
        private readonly IConfiguration _configuration;

        public UserContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Seed(builder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityRole<string>>().HasData(new IdentityRole<string>
            {
                Id = "99999",
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            modelBuilder.Entity<IdentityRole<string>>().HasData(new IdentityRole<string>
            {
                Id = "99997",
                Name = "regular",
                NormalizedName = "REGULAR"
            });

            for (int i = 1; i <= 4; i++)
            {
                AddUser(modelBuilder, i);
            }
        }

        private void AddUser(ModelBuilder modelBuilder, int number)
        {
            var userName = $"Mesa{number}";
            var email = $"mesa{number}@user.com";
            var id = $"000{number}";
            UserEntity user = new UserEntity
            {
                Id = id,
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = email,
                EmailConfirmed = true,
                NormalizedEmail = email.ToUpper(),
                TableNumber = number,
            };

            PasswordHasher<UserEntity> hasher = new PasswordHasher<UserEntity>();
            user.PasswordHash = hasher.HashPassword(user, $"@Admin{number}");
            modelBuilder.Entity<UserEntity>().HasData(user);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = id,
                RoleId = "99997"
            });
        }
    }
}
