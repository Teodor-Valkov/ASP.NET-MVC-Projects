using BirthdaySystem.Common;
using BirthdaySystem.Domain.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BirthdaySystem.Domain.SqlServer
{
    public abstract class DbRepository : IDbRepository
    {
        protected DbRepository()
            : this(SqlServerConstants.ConnectionString)
        {
        }

        protected DbRepository(string connectionString)
        {
            this.Connection = new SqlConnection(connectionString);
            this.Connection.Open();
        }

        protected SqlConnection Connection { get; private set; }

        public void Dispose()
        {
            this.Connection.Close();
        }

        protected SqlDataReader ExecuteReader(string commandText, IDictionary<string, object> parameters = null)
        {
            SqlCommand command = this.PrepareCommand(commandText, parameters);
            return command.ExecuteReader();
        }

        protected object ExecuteScalar(string commandText, IDictionary<string, object> parameters = null)
        {
            SqlCommand command = this.PrepareCommand(commandText, parameters);
            return command.ExecuteScalar();
        }

        protected int ExecuteNonQuery(string commandText, IDictionary<string, object> parameters = null)
        {
            SqlCommand command = this.PrepareCommand(commandText, parameters);
            return command.ExecuteNonQuery();
        }

        private SqlCommand PrepareCommand(string commandText, IDictionary<string, object> parameters = null)
        {
            SqlCommand command = this.Connection.CreateCommand();
            command.CommandText = commandText;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }

            return command;
        }
    }
}