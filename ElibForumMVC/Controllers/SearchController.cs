using System.Linq;
using ElibForumMVC.Data;
using ElibForumMVC.Data.Models;
using ElibForumMVC.Models.Forum;
using ElibForumMVC.Models.Post;
using ElibForumMVC.Models.Search;
using Microsoft.AspNetCore.Mvc;

namespace ElibForumMVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPost _postService;

        public SearchController(IPost postService)
        {
            _postService = postService;
        }

        public IActionResult Results(string searchQuery)
        {
            var posts = _postService.getFilteredPosts(searchQuery);
            var areNoResults = !string.IsNullOrEmpty(searchQuery) && !posts.Any();

            var postListings = posts.Select(post => new PostListingModel
            {
                id = post.Id,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new SearchResultModel
            {
                Posts = postListings,
                SearchQuery = searchQuery,
                EmptySearchResults = areNoResults
            };

            return View(model);
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                ImageUrl = forum.ImageUrl,
                Description = forum.Description
            };
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }
    }
}