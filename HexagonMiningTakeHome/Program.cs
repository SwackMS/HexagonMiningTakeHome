using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace HexagonMiningTakeHome
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "epa-http.txt";
            try
            {
                if (!File.Exists(filename))
                    throw new FileNotFoundException();
                else
                    Parse.ParseHostFile(filename);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found.");
            }
           
            Console.WriteLine("Press any key to close window");
            Console.ReadKey();
        }
    }
}
