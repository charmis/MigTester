using System.Collections.Generic;
using DataMigrationValidator.QueryListReaders;

namespace DataMigrationValidator.Factories
{
	public static  class QueryListReaderFactory
	{
		public static IEnumerable<ValidationAction> GetQueryListReader()
		{
			return new ExcelQueryListReader();
		}
	}
}
