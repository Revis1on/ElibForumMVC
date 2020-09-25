using ElibForumMVC.Data;
using ElibForumMVC.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElibForumMVC.Services
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault(user => user.Id == id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers;
        }

        public async Task SetProfileImage(string id, Uri uri)
        {
            var user = GetById(id);
            user.ProfileImageUrl = uri.AbsoluteUri;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateUserRating(string userId, Type type)
        {
            var user = GetById(userId);
            user.Rating = (int)CalculateUserRating(type, user.Rating);
            await _context.SaveChangesAsync();


        }

        private object CalculateUserRating(Type type, int userRating)
        {
            var inc = 0;
            if (type == typeof(Post))
                inc = 1;
            if (type == typeof(PostReply))
                inc = 3;

            return userRating + inc;
        }

    }
}
