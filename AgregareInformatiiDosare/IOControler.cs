using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class IOControler
    {
        private static void RemoveFile(string file)
        {
            try { File.Delete(file); }
            catch (IOException e) { Console.WriteLine(e); }
        }
        private static void RemoveFiles(string file, string appPath) 
        {
            var outputFiles = Directory.EnumerateFiles(appPath).Where(
                f => new Regex(".*" + file + ".*").IsMatch(f)
                );
            foreach (string item in outputFiles)
            {
                RemoveFile(item);
            }
        }
        public static void RemoveFiles(string appPath)
        {
            foreach (string file in Utils.OUTPUT_FILES) 
            {
                if (file.Length > 0)
                {
                    RemoveFiles(file, appPath);
                }
            }
        }
        public static string[] GetDosarNumbers()
        {
            if (!File.Exists(Utils.INPUT_FILE)) 
            {
                Console.WriteLine("Creati fisierul " +  Utils.INPUT_FILE);
                Console.WriteLine("Contine un numar de dosar pe fiecare line. ex:");
                Console.WriteLine("15365/3/2023\r\n3157/301/2024\r\n10687/94/2023");
                throw new FileNotFoundException(Utils.INPUT_FILE);
            }
            return File.ReadAllLines(Utils.INPUT_FILE);
        }
    }
}