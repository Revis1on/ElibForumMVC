using ElibForumMVC.Data;
using ElibForumMVC.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ElibForumMVC.Services
{
    class ApplicationUserService : IApplicationUser
    {
        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task IncrementRating(string id, Type type)
        {
            throw new NotImplementedException();
        }

        public Task SetProfileImage(string id, Url url)
        {
            throw new NotImplementedException();
        }
    }
}
