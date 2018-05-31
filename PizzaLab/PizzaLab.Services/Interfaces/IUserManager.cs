using PizzaLab.Models.BindingModels;
using PizzaLab.Models.Models.Users;

namespace PizzaLab.Services.Interfaces
{
    public interface IUserManager
    {
        bool CreateUser(RegisterUserBindingModel user);

        UserModel GetUser(LoginUserBindingModel userModel);
    }
}