using DataMigrationValidator.Interfaces;
using DataMigrationValidator.QueryReaders;

namespace DataMigrationValidator.Factories
{
	public static class QueryReaderFactory
	{
		public static IQueryReader GetQueryReader()
		{
			return new TextFileQueryReader();
		}
	}
}
