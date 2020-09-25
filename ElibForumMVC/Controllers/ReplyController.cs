using System;
using System.Threading.Tasks;
using ElibForumMVC.Data;
using ElibForumMVC.Data.Models;
using ElibForumMVC.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ElibForumMVC.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IPost _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        public ReplyController(IPost postService,
            UserManager<ApplicationUser> userManager,
            IApplicationUser userSerivice)
        {
            _postService = postService;
            _userManager = userManager;
            _userService = userSerivice;
        }

        public async  Task<IActionResult> Create(int id)
        {
            var post = _postService.getById(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyModel
            {
                PostConten = post.Content,
                PostTitle = post.Title,
                PostId = post.Id,

                AuthorId = user.Id,
                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ProfileImageUrl,
                AuthorRating = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),


                ForumId = post.Forum.Id,
                ForumName =post.Forum.Title,
                ForumImageUrl = post.Forum.ImageUrl,

                Created = DateTime.Now

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply (PostReplyModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);

            await _postService.AddReply(reply);
            await _userService.UpdateUserRating(userId, typeof(PostReply));

            return RedirectToAction("Index", "Post", new { id = model.PostId });

               
        }

        private object BuildReply(PostReplyModel model, ApplicationUser user)
        {
            var post = _postService.getById(model.PostId);

            return new PostReply
            {
                Post = post,
                Content = model.ReplyContent,
                Created = DateTime.Now,
                User = user
            };
        }
    }
}
