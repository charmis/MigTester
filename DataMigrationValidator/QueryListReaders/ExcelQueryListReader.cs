using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace DataMigrationValidator.QueryListReaders
{
	public class ExcelQueryListReader : IEnumerable<ValidationAction>
	{
		string m_filePath = string.Empty;

		public ExcelQueryListReader()
		{
			m_filePath = ConfigurationManager.AppSettings.Get("ExcelFilePath");			
		}

		public IEnumerator<ValidationAction> GetEnumerator()
		{
			var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", m_filePath);
			var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
			var ds = new DataSet();

			adapter.Fill(ds);

			var data = ds.Tables[0].AsEnumerable();

			var query = data.Where(x => x.Field<bool>("ShallTest") != false).Select(x =>
						new ValidationAction
						{
							SourceQueryFileName = x.Field<string>("Source"),
							DestinationQueryFileName = x.Field<string>("Destination")
						});
			
			return query.AsEnumerable<ValidationAction>().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}