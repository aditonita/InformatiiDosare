﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class SetUri
    {
//        public String Uri { get; set; }
//        public SetUri(string uriFirst, string dosarNumar)
//        {
//            Uri = uriFirst + "/SitePages/cautare.aspx?k=" + dosarNumar;
//        }

        public static string PortalURI(string dosar)
        {
            return "https://portal.just.ro" + "/SitePages/cautare.aspx?k=" + dosar;
        }

        public static string DosarInstantaURI(string IdInstanta, string IdDosar)
        {
            throw new NotImplementedException();
        }
        public static string DosarInstantaURI(string href)
        {
            throw new NotImplementedException();
        }
    }
}
