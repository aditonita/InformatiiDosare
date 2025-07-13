using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace InformatiiDosare
{
    internal class IOControler
    {
        private static void SaveLineAppend(string line, string output)
        {
            using (StreamWriter sw = new StreamWriter(output, true))
            {
                sw.WriteLine(line);
            }
        }
        //        internal static List<string> GetDosarURIs(string fileDosare, char delim)
        //        {
        //            List<string> uris = new();
        //            using (StreamReader sr = new StreamReader(fileDosare))
        //            {
        //                string? line;
        //                while ((line = sr.ReadLine()) != null)
        //                {
        //                    uris.Add(line.Split(delim)[1]);
        //                }
        //            }
        //            return uris;
        //        }
        private static void RemoveFile(string file)
        {
            try { File.Delete(file); }
            catch (IOException e)
            {
                IOControler.Logs(e.Message);
                //Console.WriteLine(e); 
            }
        }
        /// <summary>
        /// creaza fisier csv cu nrDosar si Link in portal.just.ro
        /// </summary>
        /// <param name="nrDosar"></param>
        /// <param name="uriDosar"></param>
        /// <param name="output"></param>
        /// <param name="delim"></param>
        internal static void SaveUriDosare(string nrDosar, string uriDosar, string output, char delim)
        {
            SaveLineAppend(nrDosar + delim + uriDosar, output);
        }
        /// <summary>
        /// Delete output csv files: Cai_atac.csv,Informatii_generale.csv,Instanta.csv,Parti.csv,Sedinte.csv,URI_dosare.csv
        /// </summary>
        internal static void SaveDetaliiDosare(string index, List<Dosar> dosare)
        {
            string linie = String.Empty;
            //RemoveFiles(index);
            foreach (Dosar dosar in dosare)
            {
                foreach (Instanta instanta in dosar.Instante.ListaInstante)
                {
                    //linie = String.Empty;
                    linie = "\"" + dosar.NrDosar + "\"" + Utils.CSV_DELIMITATOR +
                        "\"" + instanta.NumeIstanta + "\"" + Utils.CSV_DELIMITATOR +
                        "\"" + instanta.UriDosar + "\"" + Utils.CSV_DELIMITATOR +
                        "\"" + instanta.NrUnic + "\"" + Utils.CSV_DELIMITATOR +
                        "\"" + Utils.ConvertDateToString(instanta.DataInregistrare) + "\"" +
                        Utils.CSV_DELIMITATOR +
                        "\"" + Utils.ConvertDateToString(instanta.DataUltimaModificare) + "\"" +
                        Utils.CSV_DELIMITATOR +
                        "\"" + instanta.Sectie + "\"" + Utils.CSV_DELIMITATOR +
                        "\"" + instanta.Materie + "\"" + Utils.CSV_DELIMITATOR +
                        "\"" + instanta.Obiect + "\"" + Utils.CSV_DELIMITATOR +
                        "\"" + instanta.StadiuProcesual + "\"";
                    SaveLineAppend(linie, Utils.INFORMATII_GENERALE_FILE + index);
                    foreach (Parte parte in instanta.Parti.ListaParti)
                    {
                        //linie = String.Empty;
                        linie = "\"" + dosar.NrDosar + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + instanta.NumeIstanta + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + parte.Nume + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + parte.CalitateParte + "\"";
                        SaveLineAppend(linie, Utils.PARTI_FILE + index);
                    }
                    foreach (Sedinta sedinta in instanta.Sedinte.ListaSedinte)
                    {
                        linie = "\"" + dosar.NrDosar + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + instanta.NumeIstanta + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + Utils.ConvertDateToString(sedinta.StandardDate) + "\"" +
                            Utils.CSV_DELIMITATOR +
                            "\"" + (sedinta.OraEstimata.Hour < 10 ? "0" + sedinta.OraEstimata.Hour.ToString() :
                            sedinta.OraEstimata.Hour.ToString()) + ":"
                            + (sedinta.OraEstimata.Minute < 10 ? "0" + sedinta.OraEstimata.Minute.ToString() :
                            sedinta.OraEstimata.Minute.ToString())
                            + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + sedinta.Complet + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + sedinta.TipSolutie + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + sedinta.SolutiePeScurt + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + sedinta.Document + "\"";
                        SaveLineAppend(linie, Utils.SEDINTE_FILE + index);
                    }
                    foreach (CaleAtac caleAtac in instanta.CaiAtac.ListaCaiAtac)
                    {
                        linie = "\"" + dosar.NrDosar + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + instanta.NumeIstanta + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + Utils.ConvertDateToString(caleAtac.DataDeclarare) +
                            "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + caleAtac.ParteDeclaranta + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + caleAtac.Recurs + "\"";
                        SaveLineAppend(linie, Utils.CAI_ATAC_FILE + index);
                    }
                }
                Instanta lastInstanta = dosar.Instante.InstantaByMaxDate();
                if (lastInstanta.Sedinte != null)
                {
                    Sedinta lastSedinta = lastInstanta.Sedinte.SedintaByMaxDate();
                    linie = "\"" + dosar.NrDosar + "\"" + Utils.CSV_DELIMITATOR +
                                "\"" + lastInstanta.NumeIstanta + "\"" + Utils.CSV_DELIMITATOR +
                                "\"" + Utils.ConvertDateToString(lastSedinta.StandardDate) + "\"" +
                                Utils.CSV_DELIMITATOR +
                                "\"" + (lastSedinta.OraEstimata.Hour < 10 ? "0" + lastSedinta.OraEstimata.Hour.ToString() :
                                lastSedinta.OraEstimata.Hour.ToString()) + ":"
                                + (lastSedinta.OraEstimata.Minute < 10 ? "0" + lastSedinta.OraEstimata.Minute.ToString() :
                                lastSedinta.OraEstimata.Minute.ToString())
                                + "\"" + Utils.CSV_DELIMITATOR +
                                "\"" + lastSedinta.Complet + "\"" + Utils.CSV_DELIMITATOR +
                                "\"" + lastSedinta.TipSolutie + "\"" + Utils.CSV_DELIMITATOR +
                                "\"" + lastSedinta.SolutiePeScurt + "\"" + Utils.CSV_DELIMITATOR +
                                "\"" + lastSedinta.Document + "\"";
                }
                SaveLineAppend(linie, Utils.SITUATIE_LITIGII_FILE + index);
            }
        }
        internal static void DosareInLucru(string index, List<Dosar> dosare)
        {
            string message = String.Empty;
            foreach (Dosar dosar in dosare)
            {
                string nrDosar = dosar.NrDosar;
                string data = Utils.ConvertDateToString(dosar.Instante.InstantaByMaxDate()
                    .Sedinte.SedintaByMaxDate().StandardDate);
                    message = message + data + " --- " + nrDosar + Environment.NewLine;
            }
            SaveLineAppend(message, Utils.DOSARE_IN_LUCRU + index);
        }
        public static void Logs(String message)
        {
            SaveLineAppend(message, Utils.LOGS);
        }
    }
}