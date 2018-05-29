using System.Security.Principal;
using PaymentSystem.Models.Models.Users;

namespace PaymentSystem.Extensions
{
    public static class PrincipalExtensions
    {
        public static int GetUserId(this IPrincipal principal)
        {
            UserModel user = principal as UserModel;
            if (user != null)
            {
                return user.Id;
            }

            return 0;
        }
    }
}
