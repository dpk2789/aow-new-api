using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Aow.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser> GetUserByName(string userName)
        {
            var user = await _userManager.FindByEmailAsync(userName);
            if (user == null)
            {
                return null;
            };          

            return user;
        }
    }
}
