using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class SetUri
    {
        public String Uri { get; set; }
        public SetUri(string uriFirst, string dosarNumar) 
        {
            Uri = uriFirst + "/SitePages/cautare.aspx?k=" + dosarNumar;
        }
    }
}
