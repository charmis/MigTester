using System.Configuration;
using System.IO;
using DataMigrationValidator.Interfaces;
using System;
using System.Threading;

namespace DataMigrationValidator
{
	/// <summary>
	/// 
	/// </summary>
	public class TextResultLogger : IResultLogger
	{
		string m_filePath = "E:\\ValidationResult.txt";
		string m_fileName;

		public TextResultLogger()
		{
			//read from config the file location
			m_filePath = ConfigurationManager.AppSettings.Get("ValidationResultLocation");			
		}

		/// <summary>
		/// Logs the column count mismatch.
		/// </summary>
		public void LogColumnCountMismatch()
		{
			string s = "Source column count and Destination column count is not matching";
			WriteToLog(s);
		}

		/// <summary>
		/// Logs the column value mismatch.
		/// </summary>
		/// <param name="uniqueKeyValue">The unique key value.</param>
		/// <param name="sourceColumn">The source column.</param>
		/// <param name="srcColVal">The SRC col val.</param>
		/// <param name="destColumn">The dest column.</param>
		/// <param name="destColVal">The dest col val.</param>
		public void LogColumnValueMismatch(string uniqueKeyValue, string sourceColumn, string srcColVal, string destColumn, string destColVal)
		{
			string s = "Value Mismatch: Unique Key = " + uniqueKeyValue + " Source column = " + sourceColumn + " Source Column Value = " + srcColVal + " Destination column = " + destColumn + " Destination Column Value = " + destColVal;

			WriteToLog(s);
		}

		/// <summary>
		/// Writes to log.
		/// </summary>
		/// <param name="s">The s.</param>
		private void WriteToLog(string s)
		{
			string file = m_filePath + m_fileName;
			using (StreamWriter outfile = new StreamWriter(file, true))
			{
				outfile.WriteLine(s);
			}
		}

		/// <summary>
		/// Logs the cannot find row.
		/// </summary>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="sourceColumn">The source column.</param>
		/// <param name="srcColVal">The SRC col val.</param>
		public void LogCannotFindRow(string tableName, string sourceColumn, string srcColVal)
		{
			string s = "Cannot find value in table : " + tableName + " Source column = " + sourceColumn + " Source Column Value = " + srcColVal;
			WriteToLog(s);
		}

		/// <summary>
		/// Assigns the name of the file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		public void AssignFileName(string fileName)
		{
			m_fileName = fileName;
		}

		/// <summary>
		/// Logs the summary.
		/// </summary>
		/// <param name="summary">The summary.</param>
		public void LogSummary(string summary)
		{
			WriteToLog(summary);
		}
	}
}
