using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class SetUri
    {
//        public string Uri { get; set; }
//        public SetUri(string uriFirst, string dosarNumar)
//        {
//            Uri = uriFirst + "/SitePages/cautare.aspx?k=" + dosarNumar;
//        }
        /// <summary>
        /// returneaza cautare dosar in portal.just.ro
        /// </summary>
        /// <param name="dosar"></param>
        /// <returns>return cautare dosar URI</returns>
        public static string PortalURI(string dosar)
        {
            return "https://portal.just.ro" + "/SitePages/cautare.aspx?k=" + dosar;
            //return "https://portal.just.ro" + "/SitePages/cautare.aspx?k=" + dosar.Replace("/", "%2F") + "&v1=-mjmpdosardata";
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
