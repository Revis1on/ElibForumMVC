using ElibForumMVC.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElibForumMVC.Data
{
    public  class DataSeeder
    {
        private AplicationDbContext _context;
        public DataSeeder(AplicationDbContext context)
         {

            _context = context;

        }

        public  Task SeedSuperUser()
        {
            var roleStore = new RoleStore<IdentityRole>(_context);
            var userStore = new UserStore<ApplicationUser>(_context);

            var user = new ApplicationUser
            {

                UserName = "ForumAdmin",
                NormalizedUserName = "forumadmin",
                Email = "admin@elibforum.com",
                NormalizedEmail = "admin@elibforum.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                MemberSince = DateTime.UtcNow.AddYears(-1)


            };

            var hasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = hasher.HashPassword(user, "admin");
            user.PasswordHash = hashedPassword;


            var hasAdminRole = _context.Roles.Any(roles => roles.Name == "Admin");

            if (!hasAdminRole)
            {
                roleStore.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "admin" });
            }

            var hasSuperUser = _context.Users.Any(u => u.NormalizedUserName == user.UserName);
            if (!hasSuperUser)
            {
                 userStore.CreateAsync(user);
                 userStore.AddToRoleAsync(user, "Admin");
            }

            //_context.SaveChangesAsync();

            return Task.CompletedTask;

        }

    }
}
