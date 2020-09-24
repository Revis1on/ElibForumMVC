
using ElibForumMVC.Data;
using ElibForumMVC.Data.Models;
using ElibForumMVC.Models.ApplicationUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ElibForumMVC.Controllers
{
    public class ProfileController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload  _uploadService;
        private readonly IConfiguration _configuration;
        public ProfileController(UserManager<ApplicationUser> userManager,
            IApplicationUser userService, 
            IUpload uploadService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
            _configuration = configuration;
             

        }

        public IActionResult Detail(string id)
        {
            var user = _userService.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var model = new ProfileModel()
            {

                UserId = user.Id,
                UserName = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                MemberSince = user.MemberSince,
                IsAdmin = userRoles.Contains("Admin")

            };


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {

            var userId = _userManager.GetUserId(User);

            var connectionString = _configuration.GetConnectionString("AzureBlobStorage");

            var container = _uploadService.GetBlobContainer(connectionString);

            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            var filename = contentDisposition.FileName.Trim('"');

            var blockBlob = container.GetBlockBlobReference(filename);

            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            await _userService.SetProfileImage(userId, blockBlob.Uri);

            return RedirectToAction("Detail", "Profile", new { id = userId });
        }
    }
}
