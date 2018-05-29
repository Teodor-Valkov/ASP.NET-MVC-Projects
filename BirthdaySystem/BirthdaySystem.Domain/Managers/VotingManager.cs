using System;
using System.Collections.Generic;
using System.Linq;
using BirthdaySystem.Domain.Interfaces;
using BirthdaySystem.Domain.SqlServer;
using BirthdaySystem.Models.Models.Employees;
using BirthdaySystem.Models.Models.Presents;
using BirthdaySystem.Models.ViewModels.Votings;

namespace BirthdaySystem.Domain.Managers
{
    public class VotingManager : IVotingManager
    {
        private IVotingRepository votingRepository;
        private IEmployeeRepository employeeRepository;
        private IPresentRepository presentRepository;

        public VotingManager()
            : this(new SqlVotingRepository(), new SqlEmployeeRepository(), new SqlPresentRepository())
        {
        }

        public VotingManager(IVotingRepository votingRepository, IEmployeeRepository employeeRepository, IPresentRepository presentRepository)
        {
            this.votingRepository = votingRepository;
            this.employeeRepository = employeeRepository;
            this.presentRepository = presentRepository;
        }

        public ICollection<VotingViewModel> GetAllVotingsExceptForCurrentUserByIsClosed(string username, bool isClosed)
        {
            using (this.votingRepository)
            {
                IList<VotingViewModel> votings = this.votingRepository.GetAllVotingsExceptForCurrentUserByIsClosed(username, isClosed).ToList();

                for (int i = 0; i < votings.Count; i++)
                {
                    VotingViewModel voting = votings[i];
                    voting.Presents = voting.Presents.OrderByDescending(p => p.GiverNames.Count).ToList();
                }

                return votings;
            }
        }

        public ICollection<EmployeeDescription> GetAllEmployeesExceptForCurrentUser(string username)
        {
            using (this.employeeRepository)
            {
                ICollection<EmployeeDescription> employees = this.employeeRepository.GetAllEmployeesExceptForCurrentUser(username);
                return employees;
            }
        }

        public ICollection<PresentDescription> GetAllPresents()
        {
            using (this.presentRepository)
            {
                ICollection<PresentDescription> presents = this.presentRepository.GetAllPresents();
                return presents;
            }
        }

        public bool MakeVoting(int creatorId, int receiverId, int presentId)
        {
            if (creatorId == receiverId)
            {
                return false;
            }

            DateTime? receiverBirthDate = null;

            using (this.employeeRepository)
            {
                bool isReceiverExisting = this.employeeRepository.IsEmployeeExisting(receiverId);
                if (!isReceiverExisting)
                {
                    return false;
                }

                receiverBirthDate = this.employeeRepository.GetEmployeeBirthDate(receiverId);
            }

            using (this.presentRepository)
            {
                bool isPresentExisting = this.presentRepository.IsPresentExisting(presentId);
                if (!isPresentExisting)
                {
                    return false;
                }
            }

            using (this.votingRepository)
            {
                DateTime closingDate = new DateTime(DateTime.Now.Year, receiverBirthDate.Value.Month, receiverBirthDate.Value.Day);
                closingDate = closingDate.AddDays(-2);

                bool isVotingAlreadyExistingForThisYear = this.votingRepository.IsVotingAlreadyExistingForThisYear(receiverId, closingDate.Year);
                if (isVotingAlreadyExistingForThisYear)
                {
                    return false;
                }

                bool makeVotingResult = this.votingRepository.MakeVoting(creatorId, receiverId, presentId, closingDate);
                return makeVotingResult;
            }
        }

        public bool CanEmployeeVoteInVoting(int votingId, int userId)
        {
            using (this.votingRepository)
            {
                bool isVotingExisting = this.votingRepository.IsVotingExisting(votingId);
                if (!isVotingExisting)
                {
                    return false;
                }

                bool isEmployeeReceiver = this.votingRepository.IsEmployeeReceiverInVoting(votingId, userId);
                bool isEmployeeAlreadyVoted = this.votingRepository.IsEmployeeAlreadyVotedInVoting(votingId, userId);
                if (isEmployeeReceiver || isEmployeeAlreadyVoted)
                {
                    return false;
                }

                return true;
            }
        }

        public bool Vote(int votingId, int userId, int presentId)
        {
            using (this.presentRepository)
            {
                bool isPresentExisting = this.presentRepository.IsPresentExisting(presentId);
                if (!isPresentExisting)
                {
                    return false;
                }
            }

            using (this.votingRepository)
            {
                bool isVotingExisting = this.votingRepository.IsVotingExisting(votingId);
                if (!isVotingExisting)
                {
                    return false;
                }

                bool voteResult = this.votingRepository.Vote(votingId, userId, presentId);
                return voteResult;
            }
        }

        public bool Close(int votingId, int userId)
        {
            using (this.votingRepository)
            {
                bool isVotingExisting = this.votingRepository.IsVotingExisting(votingId);
                if (!isVotingExisting)
                {
                    return false;
                }

                bool isCurrentEmployeeCreator = this.votingRepository.IsCurrentEmployeeCreator(votingId, userId);
                if (!isCurrentEmployeeCreator)
                {
                    return false;
                }

                bool doesVotingHaveMoreThanOneVote = this.votingRepository.DoesVotingHaveMoreThanOneVote(votingId);
                if (!doesVotingHaveMoreThanOneVote)
                {
                    return false;
                }

                int presentId = this.votingRepository.GetMostVotedPresentId(votingId);
                DateTime closingDate = DateTime.Now;

                bool closeResult = this.votingRepository.CloseVoting(votingId, presentId, closingDate);
                return closeResult;
            }
        }
    }
}