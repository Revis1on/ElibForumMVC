using ElibForumMVC.Data;
using ElibForumMVC.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElibForumMVC.Services
{
    public class ForumService : IForum
    {
        private readonly  AplicationDbContext _context;
        public ForumService(AplicationDbContext context)
        {
            _context = context;

        }

        public Task Create(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumID)
        {
            throw new NotImplementedException();
        }

        public Task DeleteForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.Include(forum => forum.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forum GetById(int id)
        {
            var forum = _context.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts)
                    .ThenInclude(p => p.User)
                .Include(f => f.Posts)
                    .ThenInclude(p => p.Replies)
                        .ThenInclude(r => r.User)
                .FirstOrDefault();

            return forum;
        }

        public Task UpdateForumTitile(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
