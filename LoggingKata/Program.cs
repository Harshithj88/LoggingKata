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
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            var path = Environment.CurrentDirectory + "\\Taco_Bell-US-AL-Alabama.csv";
            if (path.Length == 0)
            {
                Console.WriteLine("You must provide a filename as an argument");
                Logger.Fatal("Cannot import without filename specified as an argument");
                Console.ReadLine();
                return;
            }
            Logger.Info("Log initialized");
            Logger.Info("Grabbing form path:" + path);
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
            ITrackable a = null;
            ITrackable b = null;
            double distance = 0;

            foreach (var locA in locations)
            {
                Logger.Debug("Checking Origin Location");
                var origin = new Coordinate
                {
                    Latitude = locA.Location.Latitude,
                    Longitude = locA.Location.Longitude
                };
                foreach (var locB in locations)
                {
                    Logger.Debug("Checking origin against destination location");
                    var destination = new Coordinate
                    {
                        Latitude = locB.Location.Latitude,
                        Longitude = locB.Location.Longitude
                    };
                    Logger.Debug("Getting distance in miles");
                    var nDistance = GeoCalculator.GetDistance(origin, destination);
                    if (nDistance > distance)
                    {
                        Logger.Info("Found the next furthest part");
                        a = locA;
                        b = locB;
                        distance = nDistance;
                    }
                }
            }

            if (a == null || b == null)
            {
                Logger.Error("Failed to find the farthest location");
                Console.WriteLine("Couldn't find the furthest apart");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"The two tacobells that are farthest apart are: {a.Name} and {b.Name}");
            Console.WriteLine($"These two locations are {distance} apart");
            Console.ReadLine();
        }
    }
}