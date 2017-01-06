using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PecoOnlineScraper.Data;

namespace PecoOnlineScraper.Save
{
    public class ResultsSave
    {
        private readonly string connectionString;
        public ResultsSave(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SaveResults(SearchData data)
        {
           SqlConnection connection = new SqlConnection(connectionString);
           try
            {
                connection.Open();
                int searchId = SaveMetadata(connection, data.Metadata);
                SaveData(connection, searchId, data.JudetResults);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private SqlCommand GetInsertDataCommand(SqlConnection connection)
        {
            SqlTransaction transaction = connection.BeginTransaction();
            string commandText = "INSERT INTO cautare_peco_date(id_cautare, judet, valoare) VALUES(@id, @jud, @val);";
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = commandText;
            command.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int));
            command.Parameters.Add(new SqlParameter("jud", System.Data.SqlDbType.VarChar));
            command.Parameters.Add(new SqlParameter("val", System.Data.SqlDbType.Float));
            command.Parameters["jud"].Size = 50;
            command.Prepare();
            return command;
        }

        private SqlCommand GetInsertMetadataCommand(SqlConnection connection)
        {
            string commandText = "INSERT INTO cautare_peco_metadate(data_cautare) VALUES (@when); SELECT SCOPE_ID();";
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = commandText;
            command.Parameters.Add(new SqlParameter("when", System.Data.SqlDbType.DateTime));
            command.Prepare();
            return command;
        }

        private void SaveData(SqlConnection connection, int searchId, IDictionary<string, IEnumerable<double>> data)
        {
            SqlCommand command = GetInsertDataCommand(connection);
            try
            {
                command.Parameters["id"].Value = searchId;
                foreach(var kv in data)
                {
                    command.Parameters["jud"].Value = kv.Key;
                    foreach(double pret in kv.Value)
                    {
                        command.Parameters["val"].Value = pret;
                        command.ExecuteNonQuery();
                    }
                }
                command.Transaction.Commit();
            }
            catch(Exception e)
            {
                command.Transaction.Rollback();
                throw e;
            }
        }

        private int SaveMetadata(SqlConnection connection, SearchMetadata metadata)
        {
            SqlCommand command = GetInsertMetadataCommand(connection);
            try
            {
                command.Parameters["when"].Value = metadata.SearchTime;
                int searchId = (int)command.ExecuteScalar();
                command.Transaction.Commit();
                return searchId;
            }
            catch(Exception e)
            {
                command.Transaction.Rollback();
                throw e;
            }    
        }
    }
}
