using ElibForumMVC.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElibForumMVC.Data
{
    public interface IPost
    {
        Post getById(int id);
        IEnumerable<Post> getAll();
        IEnumerable<Post> getFilteredPosts(Forum forum, string searchQuery);
        IEnumerable<Post> getFilteredPosts(string searchQuery);
        IEnumerable<Post> getPostByForum(int id);
        IEnumerable<Post> GetLatestPosts(int n);

        Task Add(Post post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);

        Task AddReply(PostReply reply);

    }
}
