using DataMigrationValidator.Interfaces;
using DataMigrationValidator.QueryExecutors;

namespace DataMigrationValidator.Factories
{
	public class QueryExecutorFactory
	{
		public static IQueryExecutor GetQueryExecutor()
		{
			return new SqlServerQueryExecutor();
		}
	}
}
