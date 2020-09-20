using ElibForumMVC.Models.Post;
using System.Collections.Generic;

namespace ElibForumMVC.Models.Forum
{
    public class ForumTopicModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<PostListingModel> Post { get; set; }

        public string SearchQuery { get; set; }
    }
}
