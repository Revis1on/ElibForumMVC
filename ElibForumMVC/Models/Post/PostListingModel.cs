using ElibForumMVC.Models.Forum;

namespace ElibForumMVC.Models.Post
{
    public class PostListingModel
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorId { get; set; }
        public string DatePosted { get; set; }

        public ForumListingModel Forum { get; set; }
        public int RepliesCount { get; set; }

    }
}
