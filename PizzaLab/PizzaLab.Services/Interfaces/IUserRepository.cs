using PizzaLab.Models.Models.Users;

namespace PizzaLab.Services.Interfaces
{
    public interface IUserRepository : IDbRepository
    {
        bool IsUserWithSameUsernameExisting(string username);

        bool CreateUser(CreateUserModel user);

        UserWithPasswordModel GetUserByUsername(string username);
    }
}