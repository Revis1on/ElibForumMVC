namespace ElibForumMVC.Models.Forum
{
    public class ForumListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; internal set; }
        public string ImageUrl { get; set; }


    }
}
