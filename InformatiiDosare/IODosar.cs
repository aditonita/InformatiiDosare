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
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }
            return result;
        }

        public static void SaveDosarData(string line, string output)
        {
            //            string file = output + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
            using (StreamWriter sw = new StreamWriter(output, true))
            {
                sw.WriteLine(line);
            }
        }

        internal static List<string> GetDosarURIs(string fileDosare, char delim)
        {
            List<string> uris = new();
            using (StreamReader sr = new StreamReader(fileDosare))
            {
                string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        uris.Add(line);
                    }
            }

            return uris;
        }
    }
}