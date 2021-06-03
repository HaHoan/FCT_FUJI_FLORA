using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCT_FUJI_FLORA
{
    public static class Constants
    {
        public static string PATH_CONFIG = @"SOFTWARE\FCT_FUJI_FLORA\Configs";
        public static string FILE_INPUT_EXTENSION = "*.csv";
        public static string FILE_OUTPUT_EXTENSION = "*.txt";
        public static string PASS = "PASS";
        public static string FAIL = "FAIL";
        public static string FILE_NAME(string u)
        {
            return "富士施乐-" + u + "V基板M3";
        }
        public static string ALLPASS = "ALL PASS";
    }

    public static class Machine
    {
        public static string FLORA = "FLORA";
        public static string ZAKURO = "ZAKURO";
    }

}
