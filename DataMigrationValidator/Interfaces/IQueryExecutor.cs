using System.Data;

namespace DataMigrationValidator.Interfaces
{
	public interface IQueryExecutor
	{
		DataTable Execute(string query, DatabaseType dbType);
	}
}