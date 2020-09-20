using ElibForumMVC.Models.Post;
using System.Collections.Generic;

namespace ElibForumMVC.Models.Home
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<PostListingModel> LatestPosts { get; set; }
    }
}
