using BirthdaySystem.Models.ViewModels.Votings;
using System;
using System.Collections.Generic;

namespace BirthdaySystem.Domain.Interfaces
{
    public interface IVotingRepository : IDbRepository
    {
        ICollection<VotingViewModel> GetAllVotingsExceptForCurrentUserByIsClosed(string username, bool isClosed);

        bool IsVotingAlreadyExistingForThisYear(int receiverId, int year);

        bool MakeVoting(int creatorId, int receiverId, int presentId, DateTime closingDate);

        bool IsVotingExisting(int votingId);

        bool IsEmployeeReceiverInVoting(int votingId, int employeeId);

        bool IsEmployeeAlreadyVotedInVoting(int votingId, int employeeId);

        bool Vote(int votingId, int userId, int presentId);

        bool IsCurrentEmployeeCreator(int votingId, int employeeId);

        bool DoesVotingHaveMoreThanOneVote(int votingId);

        int GetMostVotedPresentId(int votingId);

        bool CloseVoting(int votingId, int presentId, DateTime closingDate);
    }
}