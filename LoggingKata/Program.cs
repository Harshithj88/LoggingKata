using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.IO;
using Geolocation;

namespace LoggingKata
{
    class Program
    {
        //Why do you think we use ILog?
        private static readonly ILog Logger = 
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            var path = Environment.CurrentDirectory + "Taco_Bell-US-AL-Alabama.csv";
            if (path.Length == 0)
            {
                Console.WriteLine("You must provide a filename as an argument");
                Logger.Fatal("Cannot import without filename specified as an argument");
                Console.ReadLine();
                return;
            }
            Logger.Info("Log initialized");
            Logger.Info("Grabbing form path:" +path);
            var lines = File.ReadAllLines(path);
            if (lines.Length == 0)
            {
                Logger.Error("No Locations to check. Must have atleast one Location");
            }
            else if (lines.Length == 1)
            {
                Logger.Warn("Only one Location is provided");
            }
            var parser = new TacoParser();
            Logger.Debug("Initialized our parser");
            var locations = lines.Select(line => parser.Parse(line));
            Console.ReadLine();
        }
    }
}