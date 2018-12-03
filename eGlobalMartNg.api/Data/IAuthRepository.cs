using System.Threading.Tasks;
using eGlobalMartNg.api.Models;

namespace eGlobalMartNg.api.Data
{
    public interface IAuthRepository
    {
         Task<User> Login(string userName, string passoWord);
         Task<User> Register(User user, string passoWord);
         Task<bool> UserExist(string userName);
    }
}