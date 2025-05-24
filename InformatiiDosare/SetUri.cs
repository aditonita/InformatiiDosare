using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class SetUri
    {
        /// <summary>
        /// returneaza cautare dosar in portal.just.ro
        /// </summary>
        /// <param name="dosar"></param>
        /// <returns>return cautare dosar URI</returns>
        public static string PortalURI(string dosar)
        {
            return "https://portal.just.ro" + "/SitePages/cautare.aspx?k=" + dosar;
        }
    }
}
