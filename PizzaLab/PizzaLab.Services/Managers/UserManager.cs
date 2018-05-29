using System;
using PizzaLab.Common;
using PizzaLab.Models.BindingModels;
using PizzaLab.Models.ViewModels.Users;
using PizzaLab.Services.Interfaces;
using PizzaLab.Services.Repositories;

namespace PizzaLab.Services.Managers
{
    public class UserManager : IUserManager
    {
        private IUserRepository userRepository;

        public UserManager()
            : this(new UserRepository())
        {
        }

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool CreateUser(RegisterUserBindingModel user)
        {
            using (this.userRepository)
            {
                bool isUserWithSameUsernameExisting = this.userRepository.IsUserWithSameUsernameExisting(user.Username);
                if (isUserWithSameUsernameExisting)
                {
                    return false;
                }

                string passwordSalt = PasswordUtilities.GeneratePasswordSalt();
                string passwordHash = PasswordUtilities.GeneratePasswordHash(user.Password, passwordSalt);
                CreateUserModel createUserModel = new CreateUserModel(user.Username, user.Address, user.Phone, passwordHash, passwordSalt);

                bool createUserResult = this.userRepository.CreateUser(createUserModel);
                return createUserResult;
            }
        }

        public UserModel GetUser(LoginUserBindingModel userModel)
        {
            using (this.userRepository)
            {
                UserModel user = this.userRepository.GetUserByUsername(userModel.Username);

                if (user == null)
                {
                    return null;
                }

                string actualPasswordHash = PasswordUtilities.GeneratePasswordHash(userModel.Password, user.PasswordSalt);

                if (actualPasswordHash != user.PasswordHash)
                {
                    return null;
                }

                return user;
            }
        }
    }
}