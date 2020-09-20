using ElibForumMVC.Models.Forum;
using System.Collections.Generic;

namespace ElibForumMVC.Controllers
{
    internal class ForumIndexMOdel
    {
        public IEnumerable<ForumListingModel> ForumList { get; set; }
    }
}