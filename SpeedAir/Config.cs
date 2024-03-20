using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SpeedAir
{
    public static class Config
    {

        public static int DefaultMaxLoad
        {
            get { return int.Parse(ConfigurationManager.AppSettings["DEFAULT_MAX_LOAD"]); }
        }
        public static string OrdersSamplePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ORDERS_SAMPLE_PATH"];
            }
        }
    }

}
