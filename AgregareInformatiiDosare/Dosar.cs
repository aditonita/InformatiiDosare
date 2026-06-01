using InformatiiDosare;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgregareInformatiiDosare
{
    internal class Dosar
    {
        private DateOnly _legalFileDate;
        private string _legalFileNumber;

        public Dosar(DateOnly legalFileDate,  string legalFileNumber)
        {
            _legalFileDate = legalFileDate; _legalFileNumber = legalFileNumber;
        }
        internal static List<string[]> GetDosareByIteration(int dosareinIterarie)
        {
            List<string[]> result = new List<string[]>();
            int k = 0;
            string[] slicedDosar = new string[dosareinIterarie];
            string[] nrDosare = IOControler.GetDosarNumbers();
            for (int i = 0; i < nrDosare.Length; i++)
            {
                string arguments = string.Empty;
                if (i % dosareinIterarie == 0)
                {
                    k = i;
                    slicedDosar = nrDosare.Skip(i).Take(dosareinIterarie).ToArray();
                    result.Add(slicedDosar);
                }
            }
            return result;
        }
        internal static void RunInformatiiDosare(string appPath, List<string[]> dosareinIteratie)
        {
            int numThreads = dosareinIteratie.Count;
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            int toProcess = numThreads;
            for (int i = 0; i < numThreads; i++)
            {
                string nrDosar = string.Empty;
                foreach (string s in dosareinIteratie[i]) 
                {
                    nrDosar = nrDosar + s + " ";
                }
                new Thread(delegate ()
                {
                    string processArgs = @"/C " + Path.Combine(appPath, Utils.INFO_DOSAR) + " "
                    + Thread.CurrentThread.ManagedThreadId.ToString() + " " + nrDosar;
                    Console.WriteLine(processArgs.Substring(processArgs.IndexOf(Utils.INFO_DOSAR)));
                    Console.WriteLine("----------------");
                    var proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = @"cmd.exe",
                            Arguments = processArgs,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true,
                            WorkingDirectory = appPath,
                            WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                        }
                    };
                    proc.Start();
                    proc.WaitForExit();
                    if (Interlocked.Decrement(ref toProcess) == 0)
                        resetEvent.Set();
                }).Start();
            }
            resetEvent.WaitOne();
            Console.WriteLine("[info]-Thread Finished. Continue Agregation");
//            Console.ReadLine();
            AgregateResult(appPath);
            ShowFormDosareInLucru();
        }
        private static void ShowFormDosareInLucru()
        {
            List<Dosar> legalFileList = new List<Dosar>();
            string message = "";
            foreach (string line in File.ReadAllLines(Utils.DOSARE_IN_LUCRU + ".csv"))
            {
                string[] item = line.Split("---", StringSplitOptions.TrimEntries);
                string[] date = item[0].Split(new char[] { '.' }, StringSplitOptions.None);
                legalFileList.Add(new Dosar(
                    new DateOnly(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0])),
                    item[1]));
            }

            legalFileList.Sort((x,y) => x._legalFileDate.CompareTo(y._legalFileDate));
            foreach(Dosar item in legalFileList) 
            {
                message = message + item._legalFileDate.ToShortDateString() + "---" + item._legalFileNumber + Environment.NewLine;
            }
            //MessageBox.Show(File.ReadAllText(Utils.DOSARE_IN_LUCRU + ".csv"));
            MessageBox.Show(message);
        }
        private static void AgregateResult(string appPath)
        {
            List<string> list = new List<string>();
            foreach (string filePattern in Utils.OUTPUT_FILES) 
            {
                if(filePattern.Length == 0)
                {
                    continue;
                }
                var outputFiles = Directory.EnumerateFiles(appPath).Where(
                    f => new Regex(".*" + filePattern + ".*").IsMatch(f)
                    );
//                Console.WriteLine(outputFiles.Count<string>());
                foreach (string item in outputFiles.ToList<string>())
                {
//                    Console.WriteLine(item);
                    string[] lines = File.ReadAllLines(item);
                    list.AddRange(lines);
                }
                list = list.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
                File.WriteAllLines(filePattern + ".csv", list.ToArray());
                list.Clear();
            }
        }
    }
}
