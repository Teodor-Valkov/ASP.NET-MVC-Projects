using System.Collections.Generic;
using System.Data.SqlClient;
using PizzaLab.Common;
using PizzaLab.Services.Interfaces;

namespace PizzaLab.Services.Repositories
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

        protected SqlDataReader ExecuteReader(string commandText, IDictionary<string, object> parameters = null)
        {
            SqlCommand command = this.GetCommand(commandText, parameters);
            return command.ExecuteReader();
        }

        protected object ExecuteScalar(string commandText, IDictionary<string, object> parameters = null)
        {
            SqlCommand command = this.GetCommand(commandText, parameters);
            return command.ExecuteScalar();
        }

        protected int ExecuteNonQuery(string commandText, IDictionary<string, object> parameters = null)
        {
            SqlCommand command = this.GetCommand(commandText, parameters);
            return command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            this.Connection.Close();
        }

        private SqlCommand GetCommand(string commandText, IDictionary<string, object> parameters)
        {
            SqlCommand command = this.Connection.CreateCommand();
            command.CommandText = commandText;

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }

            return command;
        }
    }
}