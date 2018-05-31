using PaymentSystem.Common;
using PaymentSystem.Domain.Interfaces;
using PaymentSystem.Domain.SqlServer;
using PaymentSystem.Models.BindingModels.Users;
using PaymentSystem.Models.Models.Users;

namespace PaymentSystem.Domain.Managers
{
    public class UserManager : IUserManager
    {
        private IUserRepository userRepository;

        public UserManager()
            : this(new SqlUserRepository())
        {
        }

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool CreateUser(UserRegisterBindingModel userModel)
        {
            using (this.userRepository)
            {
                bool isUserWithSameUsernameExisting = this.userRepository.IsUserWithSameUsernameExisting(userModel.Username);
                if (isUserWithSameUsernameExisting)
                {
                    return false;
                }

                string passwordSalt = PasswordUtilities.GeneratePasswordSalt();
                string passwordHash = PasswordUtilities.GeneratePasswordHash(userModel.Password, passwordSalt);
                UserCreateModel user = new UserCreateModel(userModel.Username, userModel.Name, passwordHash, passwordSalt);

                bool createUserResult = this.userRepository.CreateUser(user);
                return createUserResult;
            }
        }

        public UserModel GetUser(UserLoginBindingModel userModel)
        {
            using (this.userRepository)
            {
                UserWithPasswordModel userWithPassword = this.userRepository.GetUserWithPasswordByUsername(userModel.Username);
                if (userWithPassword == null)
                {
                    return null;
                }

                string actualPasswordHash = PasswordUtilities.GeneratePasswordHash(userModel.Password, userWithPassword.PasswordSalt);
                if (actualPasswordHash != userWithPassword.PasswordHash)
                {
                    return null;
                }

                UserModel user = new UserModel(userWithPassword.Id, userWithPassword.Username);
                return user;
            }
        }
    }
}