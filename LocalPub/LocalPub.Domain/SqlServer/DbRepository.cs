using LocalPub.Domain.Interfaces;
using LocalPub.Common;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LocalPub.Domain.SqlServer
{
    public abstract class DbRepository : IDbRepository
    {
        public DbRepository()
            : this(SqlServerConstants.ConnectionString)
        {
        }

        public DbRepository(string connectionString)
        {
            this.Connection = new SqlConnection(connectionString);
            this.Connection.Open();
        }

        protected SqlConnection Connection { get; private set; }

        protected SqlDataReader ExecuteReader(string commandText, IDictionary<string, object> parameters = null)
        {
            var command = this.PrepareCommand(commandText, parameters);
            return command.ExecuteReader();
        }

        protected object ExecuteScalar(string commandText, IDictionary<string, object> parameters = null)
        {
            var command = this.PrepareCommand(commandText, parameters);
            return command.ExecuteScalar();
        }

        protected int ExecuteNonQuery(string commandText, IDictionary<string, object> parameters = null)
        {
            var command = this.PrepareCommand(commandText, parameters);
            return command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            this.Connection.Close();
        }

        private SqlCommand PrepareCommand(string commandText, IDictionary<string, object> parameters = null)
        {
            var command = this.Connection.CreateCommand();
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