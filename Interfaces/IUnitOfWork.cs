using System.Threading.Tasks;

namespace Identity_JWT_API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        Task<bool> Complete();
        bool HasChanges();
    }
}