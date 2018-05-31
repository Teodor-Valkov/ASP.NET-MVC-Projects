using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BirthdaySystem.Domain.Interfaces;
using BirthdaySystem.Models.Models.Presents;
using BirthdaySystem.Models.ViewModels.Votings;

namespace BirthdaySystem.Domain.SqlServer
{
    public class SqlVotingRepository : DbRepository, IVotingRepository
    {
        public SqlVotingRepository()
        {
        }

        public SqlVotingRepository(string connectionString)
            : base(connectionString)
        {
        }

        public ICollection<VotingViewModel> GetAllVotingsExceptForCurrentUserByIsClosed(string username, bool isClosed)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT vote.Id,
                          vote.CreatorUsername, vote.CreatorName,
                          vote.ReceiverUsername, vote.ReceiverName,
                          eg.Username AS GiverUsername, eg.Name AS GiverName,
                          p.Name AS PresentName, vote.ClosingDate
                     FROM
                     	(SELECT v.Id, v.ClosingDate,
                                 ec.Username AS CreatorUsername, ec.Name AS CreatorName,
                                 er.Username AS ReceiverUsername, er.Name AS ReceiverName
                     	   FROM Votings AS v
                     	   JOIN Employees AS ec
                     	     ON v.CreatorId = ec.Id
                     	   JOIN Employees AS er
                     	     ON v.ReceiverId = er.Id
                     	  WHERE er.Username != @username
                            AND v.IsClosed = @isClosed
                     	) AS vote
                     JOIN VotingPresents AS vp
                       ON vp.VotingId = vote.Id
                     JOIN Presents AS p
                       ON vp.PresentId = p.Id
                     JOIN Employees AS eg
                       ON vp.EmployeeId = eg.Id",
                      new Dictionary<string, object>
                      {
                          { "@username", username },
                          { "@isClosed", isClosed }
                      });

            ICollection<VotingViewModel> votings = new List<VotingViewModel>();

            using (reader)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string creatorUsername = reader.GetString(1);
                    string creatorName = reader.GetString(2);
                    string receiverUsername = reader.GetString(3);
                    string receiverName = reader.GetString(4);
                    string giverUsername = reader.GetString(5);
                    string giverName = reader.GetString(6);
                    string presentName = reader.GetString(7);
                    DateTime closingDate = reader.GetDateTime(8);

                    string creatorNameRepresentation = this.CombineUsernameAndName(creatorUsername, creatorName);
                    string receiverNameRepresentation = this.CombineUsernameAndName(receiverUsername, receiverName);
                    string giverNameRepresentation = this.CombineUsernameAndName(giverUsername, giverName);

                    VotingViewModel voting = null;

                    if (votings.Any(v => v.Id == id))
                    {
                        voting = votings.First(v => v.Id == id);
                    }
                    else
                    {
                        voting = new VotingViewModel(id, closingDate, creatorNameRepresentation, receiverNameRepresentation, isClosed);
                        votings.Add(voting);
                    }

                    PresentWithGiversDescription present = null;

                    if (voting.Presents.Any(p => p.Name == presentName))
                    {
                        present = voting.Presents.First(p => p.Name == presentName);
                    }
                    else
                    {
                        present = new PresentWithGiversDescription(presentName);
                        voting.Presents.Add(present);
                    }

                    present.GiverNames.Add(giverNameRepresentation);
                }
            }

            return votings;
        }

        public bool IsVotingAlreadyExistingForThisYear(int receiverId, int closingYear)
        {
            int votingsCount = (int)this.ExecuteScalar(
                    @"SELECT COUNT(*) AS VotingsCount
                        FROM Votings
                       WHERE ReceiverId = @receiverId
                   	     AND YEAR(ClosingDate) = @closingYear",
                         new Dictionary<string, object>()
                         {
                             { "@receiverId", receiverId },
                             { "@closingYear", closingYear }
                         });

            return votingsCount > 0;
        }

        public bool MakeVoting(int creatorId, int receiverId, int presentId, DateTime closingDate)
        {
            int votingId = (int)this.ExecuteScalar(
                @"INSERT INTO Votings (CreatorId, ReceiverId, ClosingDate, IsClosed)
	                   OUTPUT INSERTED.ID
                       VALUES (@creatorId, @receiverId, @closingDate, 0)",
                       new Dictionary<string, object>
                       {
                         { "@creatorId", creatorId },
                         { "@receiverId", receiverId },
                         { "@closingDate", closingDate },
                       });

            int recordsInserted = this.ExecuteNonQuery(
                  @"INSERT INTO VotingPresents (VotingId, EmployeeId, PresentId)
                         VALUES (@votingId, @creatorId, @presentId)",
                         new Dictionary<string, object>
                         {
                               { "@votingId", votingId },
                               { "@creatorId", creatorId },
                               { "@presentId", presentId },
                         });

            return recordsInserted > 0;
        }

        public bool IsVotingExisting(int votingId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id
                     FROM Votings
                    WHERE Id = @votingId",
                      new Dictionary<string, object>()
                      {
                          { "@votingId", votingId }
                      });

            using (reader)
            {
                if (reader.Read())
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsEmployeeReceiverInVoting(int votingId, int employeeId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT ReceiverId
                     FROM Votings
                    WHERE Id = @votingId",
                      new Dictionary<string, object>()
                      {
                          { "@votingId", votingId }
                      });

            using (reader)
            {
                if (reader.Read())
                {
                    int receiverId = reader.GetInt32(0);
                    return receiverId == employeeId;
                }

                return false;
            }
        }

        public bool IsEmployeeAlreadyVotedInVoting(int votingId, int employeeId)
        {
            int votingsCount = (int)this.ExecuteScalar(
                    @"SELECT COUNT(*) AS VotingsCount
                        FROM Votings AS v
                        JOIN VotingPresents AS vp
                          ON v.Id = vp.VotingId
                       WHERE v.Id = @votingId
                         AND vp.EmployeeId = @employeeId",
                         new Dictionary<string, object>()
                         {
                             { "@votingId", votingId },
                             { "@employeeId", employeeId }
                         });

            return votingsCount > 0;
        }

        public bool Vote(int votingId, int employeeId, int presentId)
        {
            int recordsInserted = this.ExecuteNonQuery(
                  @"INSERT INTO VotingPresents (VotingId, EmployeeId, PresentId)
                         VALUES (@votingId, @employeeId, @presentId)",
                         new Dictionary<string, object>
                         {
                               { "@votingId", votingId },
                               { "@employeeId", employeeId },
                               { "@presentId", presentId }
                         });

            return recordsInserted > 0;
        }

        public bool IsCurrentEmployeeCreator(int votingId, int employeeId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT CreatorId
                     FROM Votings
                    WHERE Id = @votingId",
                    new Dictionary<string, object>()
                    {
                         { "@votingId", votingId }
                    });

            using (reader)
            {
                if (reader.Read())
                {
                    int creatorId = reader.GetInt32(0);
                    return creatorId == employeeId;
                }

                return false;
            }
        }

        public bool DoesVotingHaveMoreThanOneVote(int votingId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT COUNT(vp.PresentId)
                     FROM Votings AS v
                     JOIN VotingPresents AS vp
                       ON v.Id = vp.VotingId
                    WHERE v.Id = @votingId",
                      new Dictionary<string, object>()
                      {
                          { "@votingId", votingId },
                      });

            using (reader)
            {
                if (reader.Read())
                {
                    int votesCount = reader.GetInt32(0);
                    return votesCount > 1;
                }

                return false;
            }
        }

        public int GetMostVotedPresentId(int votingId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT TOP 1
                   	      vp.PresentId,
                          COUNT(vp.presentId) AS PresentVotes
                     FROM Votings AS v
                     JOIN VotingPresents AS vp
                       ON v.Id = vp.VotingId
                    WHERE v.Id = @votingId
                    GROUP BY vp.PresentId
                    ORDER BY PresentVotes DESC",
                      new Dictionary<string, object>()
                      {
                           { "@votingId", votingId },
                     });

            using (reader)
            {
                if (reader.Read())
                {
                    int presentId = reader.GetInt32(0);
                    return presentId;
                }

                return 0;
            }
        }

        public bool CloseVoting(int votingId, int presentId, DateTime closingDate)
        {
            int rowsUpdated = this.ExecuteNonQuery(
                   @"UPDATE Votings
                        SET IsClosed = 1,
                            PresentId = @presentId,
                            ClosingDate = @closingDate
                      WHERE Id = @votingId
                        AND IsClosed = 0",
                        new Dictionary<string, object>()
                        {
                            { "@votingId", votingId },
                            { "@presentId", presentId },
                            { "@closingDate", closingDate }
                        });

            return rowsUpdated > 0;
        }

        private string CombineUsernameAndName(string username, string name)
        {
            string resultName = string.Format("{0} ({1})", string.IsNullOrEmpty(username) ? "[no name]" : name, username);
            return resultName;
        }
    }
}