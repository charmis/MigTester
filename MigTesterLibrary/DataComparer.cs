using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using DataMigrationValidator.Interfaces;
using System.Threading;


namespace DataMigrationValidator
{
	/// <summary>
	/// 
	/// </summary>
	public class DataComparer
	{
		private IResultLogger m_logger = null;

		private Stopwatch m_findWatch;
		private Stopwatch m_compareWatch;

		public DataComparer(IResultLogger logger)
		{
			m_logger = logger;
			m_findWatch = new Stopwatch();
			m_compareWatch = new Stopwatch();
		}

		/// <summary>
		/// Compares the specified source table.
		/// </summary>
		/// <param name="sourceTable">The source table.</param>
		/// <param name="destinationTable">The destination table.</param>
		public void Compare(DataTable sourceTable, DataTable destinationTable)
		{
			Console.WriteLine("No of source records = " + sourceTable.Rows.Count.ToString());
			Console.WriteLine("No of destination records = " + destinationTable.Rows.Count.ToString());

			if (sourceTable.Columns.Count != destinationTable.Columns.Count)
			{
				m_logger.LogColumnCountMismatch();
			}
			else
			{
				foreach (DataRow row in sourceTable.Rows)
				{
					m_findWatch.Start();

					//get the row from 2nd table
					DataRow secondRow = destinationTable.Rows.Find(row[0]);

					m_findWatch.Stop();

					if (secondRow != null)
					{
						//loop through all columns and validate data; assume both table have the columns in same order
						for (int i = 0; i < sourceTable.Columns.Count; i++)
						{
							m_compareWatch.Start();

							if (row[i].ToString().Trim() != secondRow[i].ToString().Trim())
							{
								m_logger.LogColumnValueMismatch(row[0].ToString(), sourceTable.Columns[i].ColumnName, row[i].ToString().Trim(), destinationTable.Columns[i].ColumnName, secondRow[i].ToString().Trim());
							}

							m_compareWatch.Stop();
						}
					}
					else
					{
						m_logger.LogCannotFindRow("Destination Table", sourceTable.Columns[0].ColumnName, row[0].ToString().Trim());
					}
					//sourceTable.Rows.Remove(row);
					//destinationTable.Rows.Remove(secondRow);
				}
			}

			//finally finding any orphan records available in second datatable
			IEnumerable<string> idsInA = sourceTable.AsEnumerable().Select(currentRow => currentRow[0].ToString());
			IEnumerable<string> idsInB = destinationTable.AsEnumerable().Select(currentRow => currentRow[0].ToString());
			IEnumerable<string> bNotA = idsInB.Except(idsInA);

			if (bNotA != null && bNotA.Count() > 0)
			{
				foreach (var id in bNotA)
				{
					m_logger.LogCannotFindRow("first Table", sourceTable.Columns[0].ColumnName, id.ToString());
				} 
			}			

			sourceTable.Dispose();
			destinationTable.Dispose();

			Console.WriteLine("Find takes = " + m_findWatch.ElapsedMilliseconds.ToString() + " ms");
			Console.WriteLine("Comparison takes = " + m_compareWatch.ElapsedMilliseconds.ToString() + " ms");
			Console.WriteLine("Current Thread ID= " + Thread.CurrentThread.ManagedThreadId.ToString());

			m_findWatch.Reset();
			m_compareWatch.Reset();

		}
	}
}
