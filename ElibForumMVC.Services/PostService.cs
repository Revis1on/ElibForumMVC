using ElibForumMVC.Data;
using ElibForumMVC.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElibForumMVC.Services
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public Task AddReply(PostReply reply)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> getAll()
        {
            return _context.Posts
            .Include(post => post.User)
            .Include(post => post.Replies).ThenInclude(reply => reply.User)
            .Include(post => post.Forum);
        }

        public Post getById(int id)
        {
            return _context.Posts.Where(post => post.Id == id)
                .Include(post => post.User)
                .Include(post => post.Replies).ThenInclude(reply => reply.User)
                .Include(post => post.Forum)
                .First();
        }

        public IEnumerable<Post> getFilteredPosts(Forum forum, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery) ?
          forum.Posts :
          forum.Posts.
          Where(post => post.Title.Contains(searchQuery)
          | post.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> getFilteredPosts(string searchQuery)
        {
            return getAll().Where(post
               => post.Title.ToLower().Contains(searchQuery.ToLower())
               || post.Content.ToLower().Contains(searchQuery.ToLower()));
        }

        public IEnumerable<Post> GetLatestPosts(int n)
        {
            return getAll().OrderByDescending(post => post.Created).Take(n);
        }

        public IEnumerable<Post> getPostByForum(int id)
        {
            return _context.Forums.Where(forum => forum.Id == id).First().Posts;
        }
    }
}
