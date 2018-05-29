using System.Collections.Generic;
using BirthdaySystem.Models.Models.Employees;
using BirthdaySystem.Models.Models.Presents;
using BirthdaySystem.Models.ViewModels.Votings;

namespace BirthdaySystem.Domain.Interfaces
{
    public interface IVotingManager
    {
        ICollection<VotingViewModel> GetAllVotingsExceptForCurrentUserByIsClosed(string username, bool isClosed);

        ICollection<EmployeeDescription> GetAllEmployeesExceptForCurrentUser(string username);

        ICollection<PresentDescription> GetAllPresents();

        bool MakeVoting(int creatorId, int receiverId, int presentId);

        bool CanEmployeeVoteInVoting(int votingId, int userId);

        bool Vote(int votingId, int userId, int presentId);

        bool Close(int votingId, int userId);
    }
}