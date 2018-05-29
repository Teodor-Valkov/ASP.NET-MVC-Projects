using BirthdaySystem.Domain.Interfaces;
using BirthdaySystem.Models.Models.Presents;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BirthdaySystem.Domain.SqlServer
{
    public class SqlPresentRepository : DbRepository, IPresentRepository
    {
        public SqlPresentRepository()
        {
        }

        public SqlPresentRepository(string connectionString)
            : base(connectionString)
        {
        }

        public ICollection<PresentDescription> GetAllPresents()
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id, Name
                     FROM Presents");

            ICollection<PresentDescription> presents = new List<PresentDescription>();

            using (reader)
            {
                while (reader.Read())
                {
                    int presentId = reader.GetInt32(0);
                    string presentName = reader.GetString(1);

                    PresentDescription present = new PresentDescription(presentId, presentName);
                    presents.Add(present);
                }
            }

            return presents;
        }

        public bool IsPresentExisting(int presentId)
        {
            SqlDataReader reader = this.ExecuteReader(
                 @"SELECT Id
                     FROM Presents
                    WHERE Id = @presentId",
                      new Dictionary<string, object>()
                      {
                          { "@presentId", presentId }
                      });
                
            int? existingPresentId = null;

            using (reader)
            {
                while (reader.Read())
                {
                    existingPresentId = reader.GetInt32(0);
                }
            }

            return existingPresentId.HasValue;
        }
    }
}