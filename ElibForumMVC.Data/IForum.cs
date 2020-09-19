using ElibForumMVC.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElibForumMVC.Data
{
    public interface IForum
    {

        Forum GetById(int id);
        IEnumerable<Forum> GetAll();
        IEnumerable<ApplicationUser> GetAllActiveUsers();

        Task Create(Forum forum);
        Task Delete(int forumID);
        Task UpdateForumTitile(int forumId, string newTitle);
        Task DeleteForumDescription(int forumId, string newDescription);


    }
}
