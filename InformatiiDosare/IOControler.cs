using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class IOControler
    {
        /// <summary>
        /// numere dosar din fisierul input.csv
        /// </summary>
        /// <returns>Array of Dosar number</returns> 
        public static string[] GetDosarNumbers()
        {
            return File.ReadAllLines(Utils.INPUT_FILE);
        }
        private static void SaveDosarData(string line, string output)
        {
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
                    uris.Add(line.Split(delim)[1]);
                }
            }
            return uris;
        }
        private static void RemoveFile(string file)
        {
            try { File.Delete(file); }
            catch (IOException e) { Console.WriteLine(e); }
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
            SaveDosarData(nrDosar + delim + uriDosar, output);
        }
        /// <summary>
        /// Delete output csv files: Cai_atac.csv,Informatii_generale.csv,Instanta.csv,Parti.csv,Sedinte.csv,URI_dosare.csv
        /// </summary>
        private static void RemoveFiles()
        {
            RemoveFile(Utils.URI_FILE);
            RemoveFile(Utils.INFORMATII_GENERALE_FILE);
            RemoveFile(Utils.PARTI_FILE);
            RemoveFile(Utils.SEDINTE_FILE);
            RemoveFile(Utils.CAI_ATAC_FILE);
            RemoveFile(Utils.INSTANTA_FILE);
        }
        internal static void SaveDetaliiDosare(List<Dosar> dosare)
        {
            string linie = String.Empty;
            RemoveFiles();
            SaveDosarData(Utils.HEADER_INFORMATII_GENERALE, Utils.INFORMATII_GENERALE_FILE);
            SaveDosarData(Utils.HEADER_PARTI, Utils.PARTI_FILE);
            SaveDosarData(Utils.HEADER_SEDINTE, Utils.SEDINTE_FILE);
            SaveDosarData(Utils.HEADER_CAI_ATAC, Utils.CAI_ATAC_FILE);
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
                    SaveDosarData(linie, Utils.INFORMATII_GENERALE_FILE);
                    foreach (Parte parte in instanta.Parti.ListaParti)
                    {
                        //linie = String.Empty;
                        linie = "\"" + dosar.NrDosar + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + instanta.NumeIstanta + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + parte.Nume + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + parte.CalitateParte + "\"";
                        SaveDosarData(linie, Utils.PARTI_FILE);
                    }
                    foreach (Sedinta sedinta in instanta.Sedinte.ListaSedinte)
                    {
                        linie = "\"" + dosar.NrDosar + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + instanta.NumeIstanta + Utils.CSV_DELIMITATOR +
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
                        SaveDosarData(linie, Utils.SEDINTE_FILE);
                    }
                    foreach (CaleAtac caleAtac in instanta.CaiAtac.ListaCaiAtac)
                    {
                        linie = "\"" + dosar.NrDosar + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + instanta.NumeIstanta + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + Utils.ConvertDateToString(caleAtac.DataDeclarare) +
                            "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + caleAtac.ParteDeclaranta + "\"" + Utils.CSV_DELIMITATOR +
                            "\"" + caleAtac.Recurs;
                        SaveDosarData(linie, Utils.CAI_ATAC_FILE);
                    }
                }
            }
        }
        internal static void DosareInLucru(List<Dosar> dosare)
        {
            string message = String.Empty;
            foreach (Dosar dosar in dosare)
            {
                string nrDosar = dosar.NrDosar;
                    string data = Utils.ConvertDateToString(dosar.Instante.InstantaByMaxDate()
                        .Sedinte.SedintaByMaxDate().StandardDate);
                    message = message + data + " --- " + nrDosar + Environment.NewLine;
            }
            MessageBox.Show(message);
        }
    }
}