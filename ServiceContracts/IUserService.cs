using Entities;

namespace ServiceContracts
{
    public interface IUserService
    {
        Task<User> GetUser(string UniqueMachineId);
    }
}