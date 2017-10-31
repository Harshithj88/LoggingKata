using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using log4net;
using log4net.Core;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the TacoBells
    /// </summary>
    public class TacoParser
    {
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ITrackable Parse(string line)
        {
            //DO not fail if one record parsing fails, return null
            var items = line.Split(',');
            if (items.Length < 3)
            {
                Logger.Error("Must have atleast three items to parse into ITrackable");
                return null;
            }

            double lon = 0;
            double lat = 0;

            try
            {
                Logger.Debug("Parsing Longitude");
                lon = double.Parse(items[0]);

                Logger.Debug("Parsing Latitude");
                lat = double.Parse(items[1]);
            }
            catch (Exception e)
            {
                Logger.Error("Failed to parse the location");
                Console.WriteLine(e);
                return null;
            }
            return new TacoBell //object Initializer
            {
                Name = items[2].Split('.')[0].Replace("/", "").Replace("\"", ""),
                Location = new Point(lat, lon)
            };
        }
    }
}