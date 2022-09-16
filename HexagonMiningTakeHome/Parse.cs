using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace HexagonMiningTakeHome
{
    public class Parse
    {
        public static void ParseHostFile(string filename)
        {
            string file = GenerateTimeStamp();

            Dictionary<string, int> hostCount = new Dictionary<string, int>();
            Dictionary<string, int> uriCount = new Dictionary<string, int>();

            int temp;
            string[] subs;
            string tempStr;

            foreach (string line in File.ReadLines(filename))
            {
                subs = line.Split(' ');
                //count number of accesses for each host
                if (hostCount.ContainsKey(subs[0]))
                {
                    temp = hostCount[subs[0]];
                    temp++;
                    hostCount[subs[0]] = temp;
                }
                else
                {
                    hostCount.Add(subs[0], 1);
                }

                //clean URI name i.e. remove "
                tempStr = CleanQuotes(subs[3]);

                //count successful accesses for each URI i.e. ignore non-GET
                //request doesn't always contain a GET or POST keyword but this check will ignore requests without any keyword at all
                if (CheckIfGet(subs[2]) && CheckIfSuccess(subs[5]))
                {
                    if (uriCount.ContainsKey(tempStr))
                    {
                        temp = uriCount[tempStr];
                        temp++;
                        uriCount[tempStr] = temp;
                    }
                    else
                    {
                        uriCount.Add(tempStr, 1);
                    }
                }
            }

            //sort here
            var hostList = hostCount.OrderByDescending(key => key.Value).ToList();
            var uriList = uriCount.OrderByDescending(key => key.Value).ToList();

            WriteToFile(hostList, file);
            AppendToFile(uriList, file);
        }

        public static bool CheckIfGet(string str)
        {
            return Regex.IsMatch(str, "\"GET");
        }
        public static bool CheckIfSuccess(string str)
        {
            return Regex.IsMatch(str, "200");
        }
        public static string CleanQuotes(string str)
        {
            return Regex.Replace(str, "\"", "");
        }
        public static string GenerateTimeStamp()
        {
            DateTime dt = DateTime.Now;
            //fix to use working directory
            return @AppDomain.CurrentDomain.BaseDirectory + "output_" + dt.Year + " -" + dt.Month + "-" + dt.Day + "-" + dt.Hour + "-" + dt.Minute + "-" + dt.Second + ".txt";
        }

        private static void WriteToFile(List<KeyValuePair<string, int>> list, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                foreach (KeyValuePair<string, int> entry in list) sw.WriteLine(entry.Key + " " + entry.Value);
            }
        }

        private static void AppendToFile(List<KeyValuePair<string, int>> list, string filename)
        {
            using (StreamWriter sw = File.AppendText(filename))
            {
                foreach (KeyValuePair<string, int> entry in list) sw.WriteLine(entry.Key + " " + entry.Value);
            }
        }
    }
}
