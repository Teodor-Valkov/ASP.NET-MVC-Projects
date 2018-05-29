﻿using PaymentSystem.Models.Models.Users;

namespace PaymentSystem.Domain.Interfaces
{
    public interface IUserRepository : IDbRepository
    {
        UserWithPasswordModel GetUserWithPasswordByUsername(string username);
    }
}