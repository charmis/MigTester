using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataMigrationValidator.Interfaces;

namespace DataMigrationValidator.QueryExecutors
{
	public class SqlServerQueryExecutor : IQueryExecutor
	{
		private string m_SourceConnString;
		private string m_DestinationConnString;

		public SqlServerQueryExecutor()
		{
			m_SourceConnString = ConfigurationManager.ConnectionStrings["SourceDB"].ConnectionString;
			m_DestinationConnString = ConfigurationManager.ConnectionStrings["DestinationDB"].ConnectionString;
		}

		public DataTable Execute(string query, DatabaseType dbType)
		{
			DataTable queryResultTable = null;

			DataSet ds1 = new DataSet();

			using(SqlConnection con = new SqlConnection())
			{
				switch(dbType)
				{
					case DatabaseType.Source:
						con.ConnectionString = m_SourceConnString;
						break;

					case DatabaseType.Destination:
						con.ConnectionString = m_DestinationConnString;
						break;
				}

				SqlCommand cmd = new SqlCommand(query);
				cmd.CommandType = CommandType.Text;
				cmd.Connection = con;
				con.Open();

				queryResultTable = new DataTable("queryResultTable");

				ds1.Tables.Add(queryResultTable);
				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				adapter.Fill(queryResultTable);

				DataColumn[] keys = new DataColumn[1];
				keys[0] = queryResultTable.Columns[0];

				queryResultTable.PrimaryKey = keys;
			}

			return queryResultTable;
		}
	}
}
