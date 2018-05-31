using PaymentSystem.Models.BindingModels.Users;
using PaymentSystem.Models.Models.Users;

namespace PaymentSystem.Domain.Interfaces
{
    public interface IUserManager
    {
        bool CreateUser(UserRegisterBindingModel userModel);

        UserModel GetUser(UserLoginBindingModel userModel);
    }
}