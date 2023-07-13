using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class IODosar
    {
        public static List<string> GetNrDosare(string file)
        {
            List<string> result = new List<string>();
            using(StreamReader sr = new StreamReader(file))
            {
                string? line;
                while((line = sr.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }
            return result;
        }

        public static void SaveDosarData(string line)
        {
            string file = "output" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
            using (StreamWriter sw = new StreamWriter(file, true))
            {
                sw.WriteLine(line);
            }
        }
    }
}