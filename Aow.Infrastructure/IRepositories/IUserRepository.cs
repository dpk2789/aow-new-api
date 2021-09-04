using Aow.Infrastructure.Domain;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IUserRepository
    {
         Task<AppUser> GetUserByName(string userName);
    }
}
