using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    public enum TagName
    {
        TAG_NAME_SEDINTE,
        TAG_NAME_INFORMATII_GENERALE,
        TAG_NAME_CAI_ATAC
    }
    internal class Tag
    {
        static IConfiguration config = new ConfigurationBuilder()
         .AddJsonFile(@"appsettings.json")
         .AddEnvironmentVariables()
         .Build();
        static readonly byte[] INFORMATII_DOSAR = config.GetSection("Informatii dosar").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly byte[] SEDINTE = config.GetSection("Sedinte").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly byte[] INFORMATII_GENERALE = config.GetSection("Informatii generale").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly byte[] CAI_ATAC = config.GetSection("Cai atac").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly byte[] PARTI = config.GetSection("Parti").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly string CAUTA_DOSAR = config.GetValue<string>("UriCautaDosar") ?? "";
        public static readonly string LISTEAZA_DOSAR = config.GetValue<string>("UriPortalJust") ?? "";
        public static readonly string HEADER_INFORMATII_GENERALE = config.GetValue<string>("Header Informatii generale") ?? "";
        public static readonly string HEADER_PARTI = config.GetValue<string>("Header Parti") ?? "";
        public static readonly string HEADER_SEDINTE = config.GetValue<string>("Header Sedinte") ?? "";
        public static readonly string HEADER_CAI_ATAC = config.GetValue<string>("Header Cai atac") ?? "";
        public static bool IsTagName(string name, TagName tagName)
        {
            switch (tagName)
            {
                case TagName.TAG_NAME_SEDINTE:
                    return CheckTagName(name, Tag.SEDINTE);
                case TagName.TAG_NAME_INFORMATII_GENERALE:
                    return CheckTagName(name, Tag.INFORMATII_GENERALE);
                case TagName.TAG_NAME_CAI_ATAC:
                    return CheckTagName(name, Tag.CAI_ATAC);
                default: return false;
            }
        }
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
            return new WebHtml().GetAttributesTitle(CAUTA_DOSAR + "k=" + nrDosar, "title");
        }
        internal static string? GetAttributName(string idInstanta, string idDosar)
        {
            return new WebHtml().GetAttributesTitle(LISTEAZA_DOSAR + "/" + idInstanta + "/SitePages/Dosar.aspx?" + "id_inst=" + idInstanta + "&id_dosar=" + idDosar, "name");
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
    }
}
