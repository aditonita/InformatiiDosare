using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class IODosar
    {
        public static void GetNrDosare(string infile, string outfile, char delim)
        {
            using (StreamReader sr = new StreamReader(infile))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    string uri = SetUri.PortalURI(line);
                    IODosar.SaveUriDosare(line, new WebHtml().GetLinkDosar(uri), outfile, delim);
                }
            }
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
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        internal static void SaveUriDosare(string nrDosar, string uriDosar, string output, char delim)
        {
            SaveDosarData(nrDosar + delim + uriDosar, output);
        }

        internal static void RemoveFiles(string outputFile, string informatiiGenerale, string parti, string sedinte, string caiAtac)
        {
            RemoveFile(outputFile);
            RemoveFile(informatiiGenerale);
            RemoveFile(parti);
            RemoveFile(sedinte);
            RemoveFile(caiAtac);
        }

        internal static void SaveInformatiiGenerale(string inFile, Dictionary<string, string> outFile, char delim)
        {
            SaveDosarData(Tag.HEADER_INFORMATII_GENERALE, outFile["informatiiGenerale"]);
            SaveDosarData(Tag.HEADER_PARTI, outFile["parti"]);
            SaveDosarData(Tag.HEADER_SEDINTE, outFile["sedinte"]);
            SaveDosarData(Tag.HEADER_CAI_ATAC, outFile["caiAtac"]);
            using (StreamReader sr = new StreamReader(inFile))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] cols = line.Split(delim);
                    Dictionary<string, string>? getInformatiiDosar = new WebHtml().GetInformatiiDosar(cols[0], cols[1]);
                    if (getInformatiiDosar != null)
                    {
                        SaveDosarData(getInformatiiDosar["informatiiGenerale"], outFile["informatiiGenerale"]);
                        SaveDosarData(getInformatiiDosar["parti"], outFile["parti"]);
                        SaveDosarData(getInformatiiDosar["sedinte"], outFile["sedinte"]);
                        SaveDosarData(getInformatiiDosar["caiAtac"], outFile["caiAtac"]);
                    }
                }
            }
        }
    }
}