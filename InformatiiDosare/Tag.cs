using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Remote;
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
         .AddJsonFile($"appsettings.json")
         .AddEnvironmentVariables()
         .Build();
//        private static readonly byte[]? sedinte = config.GetSection("Sedinte").Get<byte[]>();

        //public static readonly byte[] _SEDINTE = sedinte ?? Array.Empty<byte>();

        public static readonly byte[] SEDINTE = config.GetSection("Sedinte").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly byte[] INFORMATII_GENERALE = config.GetSection("Informatii generale").Get<byte[]>() ?? Array.Empty<byte>();
        public static readonly byte[] CAI_ATAC = config.GetSection("Cai atac").Get<byte[]>() ?? Array.Empty<byte>();


//        public static readonly byte[] SEDINTE = new byte[] { 94, 101, 100, 105, 110, 99, 101 };
//        public static readonly byte[] INFORMATII_GENERALE = new byte[] { 73, 110, 102, 111, 114, 109, 97, 99, 105, 105, 95, 103, 101, 110, 101, 114, 97, 108, 101 };
//        public static readonly byte[] CAI_ATAC = new byte[] { 99, 97, 105, 32, 97, 116, 97, 99 };


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
    }
}
