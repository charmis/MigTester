using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using DataMigrationValidator;

namespace MigTesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            ILog log = LogManager.GetLogger(typeof(Program));

            Console.WriteLine("Data Validation started...");

            try
            {
                Validator v = new Validator();
                v.Validate();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                Console.WriteLine("I am sorry, Data Validation ended with errors. Please check the logs for more details.");
            }

            Console.ReadLine();
        }
    }
}
