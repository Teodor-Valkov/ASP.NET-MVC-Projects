using PizzaLab.Models.ViewModels.Users;

namespace PizzaLab.Services.Interfaces
{
    public interface IUserRepository : IDbRepository
    {
        bool IsUserWithSameUsernameExisting(string username);

        bool CreateUser(CreateUserModel user);

        UserModel GetUserByUsername(string username);
    }
}