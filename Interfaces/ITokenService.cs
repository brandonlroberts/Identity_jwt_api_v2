using System.Threading.Tasks;
using Identity_JWT_API.Extensions.Entities;

namespace Identity_JWT_API.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}