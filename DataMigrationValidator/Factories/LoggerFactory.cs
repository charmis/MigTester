using DataMigrationValidator.Interfaces;

namespace DataMigrationValidator.Factories
{
	public class LoggerFactory
	{
		public static IResultLogger GetResultLogger()
		{
			//Read from config and decide the logger

			return new TextResultLogger();
		}
	}
}
