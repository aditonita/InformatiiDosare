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
        internal List<string[]> GetDosareByIteration(int dosareinIterarie)
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
        internal void RunInformatiiDosare(string appPath, List<string[]> dosareinIteratie)
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
        private void ShowFormDosareInLucru()
        {
            MessageBox.Show(File.ReadAllText(Utils.DOSARE_IN_LUCRU + ".csv"));
        }
        private void AgregateResult(string appPath)
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
