using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElibForumMVC.Data;
using ElibForumMVC.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ElibForumMVC.Controllers
{
    public class ProfileController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload  _uploadService;
        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userService, IUpload uploadService)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
             

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
