using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InformatiiDosare
{
    internal class Utils
    {
        static IConfiguration config = new ConfigurationBuilder()
         .AddJsonFile(@"appsettings.json")
         .AddEnvironmentVariables()
         .Build();
        #region Constants
        static readonly byte[] INFORMATII_DOSAR = config.GetSection("Informatii dosar").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly byte[] SEDINTE = config.GetSection("Sedinte").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly byte[] INFORMATII_GENERALE = config.GetSection("Informatii generale").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly byte[] CAI_ATAC = config.GetSection("Cai atac").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly byte[] PARTI = config.GetSection("Parti").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly byte[] PAGINA_PRINCIPALA = config.GetSection("Pagina_principala").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly string CAUTA_DOSAR = config.GetValue<string>("UriCautaDosar") ?? "";
        public static readonly string LISTEAZA_DOSAR = config.GetValue<string>("UriPortalJust") ?? "";
        public static readonly string HEADER_INFORMATII_GENERALE = config.GetValue<string>("Header Informatii generale") ?? "";
        public static readonly string HEADER_PARTI = config.GetValue<string>("Header Parti") ?? "";
        public static readonly string HEADER_SEDINTE = config.GetValue<string>("Header Sedinte") ?? "";
        public static readonly string HEADER_CAI_ATAC = config.GetValue<string>("Header Cai atac") ?? "";
        public static readonly string HEADER_SITUATIE_LITIGII = config.GetValue<string>("Header Situatie_litigii") ?? "";
        public static readonly string INPUT_FILE = config.GetValue<string>("InputNrDosarFile") ?? "";
        public static readonly string CAI_ATAC_FILE = config.GetSection("OutputFiles").GetSection("CaiAtacFile").Value ?? "";
        public static readonly string INFORMATII_GENERALE_FILE = config.GetSection("OutputFiles").GetSection("InformatiiGeneraleFile").Value ?? "";
        public static readonly string INSTANTA_FILE = config.GetSection("OutputFiles").GetSection("InstantaFile").Value ?? "";
        public static readonly string PARTI_FILE = config.GetSection("OutputFiles").GetSection("PartiFile").Value ?? "";
        public static readonly string SEDINTE_FILE = config.GetSection("OutputFiles").GetSection("SedinteFile").Value ?? "";
        public static readonly string URI_FILE = config.GetSection("OutputFiles").GetSection("UriFile").Value ?? "";
        public static readonly char CSV_DELIMITATOR = config.GetValue<char>("CsvDelimitator");
        public static readonly string SITUATIE_LITIGII_FILE = config.GetSection("OutputFiles").GetSection("SituatieLitigii").Value ?? "";
        #endregion
        private static bool CheckTagName(string name, byte[] tagNameAsByte)
        {
            if (name != "" && name.Length == tagNameAsByte.Length)
            {
                for (int i = 0; i < name.Length; i++)
                {
                    if ((byte)name.ToCharArray()[i] != tagNameAsByte[i])
                    {
                        return false;
                    }

                }
            }
            else
            {
                return false;
            }
            return true;
        }

        internal static string GetAttributName(string nrDosar)
        {
            return WebControler.GetAttributesTitle(CAUTA_DOSAR + "k=" + nrDosar, "title");
        }
        internal static string? GetAttributName(string idInstanta, string idDosar)
        {
            return WebControler.GetAttributesTitle(LISTEAZA_DOSAR + "/" + idInstanta + "/SitePages/Dosar.aspx?" + "id_inst=" + idInstanta + "&id_dosar=" + idDosar, "name");
        }
        internal static string? GetAttributName(string idInstanta, string idDosar, string instanta)
        {
            if (instanta == "true")
            {
                return WebControler.GetAttributesTitle(LISTEAZA_DOSAR + "/" + idInstanta + "/SitePages/Dosar.aspx?" + "id_inst=" + idInstanta + "&id_dosar=" + idDosar, "title");
            }
            if (instanta == "false")
            {
                return GetAttributName(idInstanta, idDosar);
            }
            throw new Exception("Argument --instanta poate fi true sau false");
        }
        internal static bool HasTitle(string titleName)
        {
            int result = 0;
            if (titleName.Length == INFORMATII_DOSAR.Length)
            {
                for (int i = 0; i < titleName.Length; i++)
                {
                    if ((byte)titleName.ToCharArray()[i] == INFORMATII_DOSAR[i])
                    {
                        result += 1;
                    }
                }
            }
            return (result == INFORMATII_DOSAR.Length);
        }
        internal static bool HasAttribute(string name, byte[] attribute)
        {
            int result = 0;
            if (name.Length == attribute.Length)
            {
                for (int i = 0; i < name.Length; i++)
                {
                    if ((byte)name.ToCharArray()[i] == attribute[i])
                    {
                        result += 1;
                    }
                }
            }
            return (result == attribute.Length);
        }
        /// <summary>
        /// format date dd.mm.yyyy
        /// </summary>
        /// <param name="dateAsString"></param>
        /// <returns>true</returns>
        public static bool IsDateFormat(string dateAsString)
        {
            string[] date = dateAsString.Split(['.','/']);
            if (dateAsString.Length != 10) { return false; }
            if (date[0].Length != 2 && Int32.Parse(date[0]) > 31) { return false; }
            if (date[1].Length != 2 && Int32.Parse(date[1]) > 12) { return false; }
            if (date[2].Length != 4) { return false; }
            return true;
        }
        public static string ConvertDateToString(DateOnly dateOnly)
        {
            string day = dateOnly.Day < 10 ? "0" + dateOnly.Day.ToString() : dateOnly.Day.ToString();
            string month = dateOnly.Month < 10 ? "0" + dateOnly.Month.ToString() : dateOnly.Month.ToString();
            string year = dateOnly.Year < 10 ? "0" + dateOnly.Year.ToString() : dateOnly.Year.ToString();

            return day + "." + month + "." + year;
        }
        public static void Progress(int length, int contor)
        {
            if ((contor * 1000 / length) % 5 == 0)
            {
                Console.WriteLine(((contor * 100 / length)).ToString() + " %");
            }
        }
    }
}
