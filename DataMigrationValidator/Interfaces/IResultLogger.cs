
namespace DataMigrationValidator.Interfaces
{
	public interface IResultLogger
	{
		void LogColumnCountMismatch();

		void LogColumnValueMismatch(string uniqueKeyValue, string sourceColumn, string srcColVal, string destColumn, string destColVal);

		void LogCannotFindRow(string tableName, string sourceColum, string srcColVal);

		void AssignFileName(string fileName);

		void LogSummary(string p);
	}
}
