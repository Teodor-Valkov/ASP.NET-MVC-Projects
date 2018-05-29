using BirthdaySystem.Models.Models.Presents;
using System.Collections.Generic;

namespace BirthdaySystem.Domain.Interfaces
{
    public interface IPresentRepository : IDbRepository
    {
        ICollection<PresentDescription> GetAllPresents();

        bool IsPresentExisting(int presentId);
    }
}