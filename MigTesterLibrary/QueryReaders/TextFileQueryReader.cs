using System;
using System.Configuration;
using System.IO;
using DataMigrationValidator.Interfaces;

namespace DataMigrationValidator.QueryReaders
{
	public class TextFileQueryReader : IQueryReader
	{
		private string m_QueryFileLocation;

		public TextFileQueryReader()
		{
			m_QueryFileLocation = ConfigurationManager.AppSettings.Get("QueryFileLocation");
		}

		public string GetQuery(string fileName)
		{
			String line = string.Empty;

			try
			{
				using (StreamReader sr = new StreamReader(m_QueryFileLocation + fileName))
				{
					line = sr.ReadToEnd();

				}
			}
			catch (Exception e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}

			return line;
		}
	}
}
