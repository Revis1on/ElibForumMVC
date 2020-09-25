using System.Linq;
using System.Threading.Tasks;
using ElibForumMVC.Data;
using ElibForumMVC.Data.Models;
using ElibForumMVC.Models.ApplicationUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace LambdaForums.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager; //Provided by Microsoft.AspNetCore.Identity
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService; //To upload profile image to the cloud
        private readonly IConfiguration _configuration;
        public ProfileController(UserManager<ApplicationUser> userManager,
            IApplicationUser userService,
            IUpload uploadService,
            IConfiguration configuration)
        {
            this._userManager = userManager;
            this._userService = userService;
            this._uploadService = uploadService;
            this._configuration = configuration;
        }

        public IActionResult Detail(string id)
        {
            var user = _userService.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;    

            var model = new ProfileModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                MemberSince = user.MemberSince,
                IsAdmin = userRoles.Contains("Admin")
            };

            return View(model);
        }

        public IActionResult Index()
        {
            var profiles = _userService.GetAll()
                .OrderByDescending(user => user.Rating)
                .Select(u => new ProfileModel
                {
                    Email = u.Email,
                    ProfileImageUrl = u.ProfileImageUrl,
                    UserRating = u.Rating.ToString(),
                    MemberSince = u.MemberSince,
                });

            var model = new ProfileList
            {
                Profiles = profiles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);


            //Connect to Azure Storage Container
            var connectionString = _configuration.GetConnectionString("AzureBlobStorage");
            //Get Blob Container
            var container = _uploadService.GetBlobContainer(connectionString, "profile-image");
            //Parse the Content Disposition response header
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            //Grab the filename
            var filename = contentDisposition.FileName.Trim().ToString();
            //Get a reference to a Block Blob
            var blockBlob = container.GetBlockBlobReference(filename);
            //On that block blob, upload our file
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            //Set the user's profile image to the received URI
            await _userService.SetProfileImage(userId, blockBlob.Uri);
            //Redirect to user's profile page
            return RedirectToAction("Detail", "Profile", new { id = userId });
        }
    }
}