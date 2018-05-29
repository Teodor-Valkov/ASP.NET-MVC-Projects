using PaymentSystem.Models.BindingModels.Users;
using PaymentSystem.Models.Models.Users;

namespace PaymentSystem.Domain.Interfaces
{
    public interface IUserManager
    {
        UserModel GetUser(UserLoginBindingModel userModel);
    }
}