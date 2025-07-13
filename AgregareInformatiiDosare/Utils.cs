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
        public static readonly string APP_PATH = config.GetValue<string>("ApplicationPath") ?? "";
        public static readonly string INFO_DOSAR = config.GetValue<string>("InfoDosar") ?? "";
        public static readonly string HEADER_INFORMATII_GENERALE = config.GetValue<string>("Header Informatii generale") ?? "";
        public static readonly string HEADER_PARTI = config.GetValue<string>("Header Parti") ?? "";
        public static readonly string HEADER_SEDINTE = config.GetValue<string>("Header Sedinte") ?? "";
        public static readonly string HEADER_CAI_ATAC = config.GetValue<string>("Header Cai atac") ?? "";
        public static readonly string HEADER_SITUATIE_LITIGII = config.GetValue<string>("Header Situatie_litigii") ?? "";
        public static readonly string INPUT_FILE = config.GetValue<string>("InputNrDosarFile") ?? "";
        public static readonly char CSV_DELIMITATOR = config.GetValue<char>("CsvDelimitator");
        public static readonly string[] OUTPUT_FILES = new string[]
        {
            config.GetSection("OutputFiles").GetSection("CaiAtacFile").Value ?? "",
            config.GetSection("OutputFiles").GetSection("InformatiiGeneraleFile").Value ?? "",
            config.GetSection("OutputFiles").GetSection("PartiFile").Value ?? "",
            config.GetSection("OutputFiles").GetSection("SedinteFile").Value ?? "",
            config.GetSection("OutputFiles").GetSection("UriFile").Value ?? "",
            config.GetSection("OutputFiles").GetSection("SituatieLitigii").Value ?? "",
            config.GetSection("OutputFiles").GetSection("DosareInLucru").Value ?? ""
        };
        public static readonly string DOSARE_IN_LUCRU = config.GetSection("OutputFiles").GetSection("DosareInLucru").Value ?? "";
        public static readonly int DOSARE_IN_ITERATIE = config.GetValue<int>("DosareInIteratie");
        #endregion

    }
}
