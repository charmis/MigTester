using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using DataMigrationValidator.Factories;
using DataMigrationValidator.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace DataMigrationValidator
{
	/// <summary>
	/// 
	/// </summary>
	public class Validator
	{
		/// <summary>
		/// Validates this instance.
		/// </summary>
		public void Validate()
		{		
			IEnumerable<ValidationAction> reader = QueryListReaderFactory.GetQueryListReader();

			StringBuilder summary = new StringBuilder();
			
			Parallel.ForEach(reader, currentItem =>
			{
				IResultLogger logger = LoggerFactory.GetResultLogger();
				logger.AssignFileName(currentItem.SourceQueryFileName + ".txt");
				DataComparer comparer = new DataComparer(logger);

				summary.Append(Compare(comparer, currentItem));
			});

			IResultLogger summaryLogger = LoggerFactory.GetResultLogger();
			summaryLogger.AssignFileName("Summary.txt");
			summaryLogger.LogSummary(summary.ToString());
		}

		/// <summary>
		/// Compares the specified comparer.
		/// </summary>
		/// <param name="comparer">The comparer.</param>
		/// <param name="currentItem">The current item.</param>
		/// <returns></returns>
		private string Compare(DataComparer comparer, ValidationAction currentItem)
		{
			StringBuilder summary = new StringBuilder();

			Console.WriteLine("Currently executing : " + currentItem.SourceQueryFileName);

			IQueryExecutor sqlQueryExecutor = QueryExecutorFactory.GetQueryExecutor();

			IQueryReader qReader = QueryReaderFactory.GetQueryReader();

			DataTable dt1 = new DataTable();
			DataTable dt2 = new DataTable();

			dt1 = sqlQueryExecutor.Execute(qReader.GetQuery(currentItem.SourceQueryFileName), DatabaseType.Source);
			dt2 = sqlQueryExecutor.Execute(qReader.GetQuery(currentItem.DestinationQueryFileName), DatabaseType.Destination);

			Stopwatch sw = new Stopwatch();
			sw.Start();

			comparer.Compare(dt1, dt2);

			sw.Stop();

			Console.WriteLine(sw.ElapsedMilliseconds.ToString() + " ms");
			Console.WriteLine("--------------------------------------------------------");

			summary.AppendLine(currentItem.SourceQueryFileName);

			summary.Append("Number of records in Source table : ").Append(dt1.Rows.Count);
			summary.AppendLine();
			summary.Append("Number of records in Destination table : ").Append(dt2.Rows.Count);
			summary.AppendLine();
			summary.Append("Time taken : ").Append(sw.ElapsedMilliseconds);
			summary.AppendLine();
			summary.AppendLine("-----------------------------------------------------------------");

			return summary.ToString();
		}
	}
}