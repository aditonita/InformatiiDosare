using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class CaleAtac
    {
        private DateOnly _dataDeclarare;
        private String _parteDeclaranta;
        private String _caleAtac;

        public DateOnly GetDataDeclarare { get { return _dataDeclarare; } }
        public String GetParteDeclaranta { get { return _parteDeclaranta; } }
        public String GetCaleAtac {get {return _caleAtac;} }
        public CaleAtac(String dataDeclarare, String parteDeclaranta, String caleAtac)
        {
            dataDeclarare = dataDeclarare.Trim();
            parteDeclaranta = parteDeclaranta.Trim();
            caleAtac = caleAtac.Trim();
            _dataDeclarare = new DateOnly(Int32.Parse(dataDeclarare.Substring(6,4)), Int32.Parse(dataDeclarare.Substring(3,2)), Int32.Parse(dataDeclarare.Substring(0,2)));
            _parteDeclaranta = parteDeclaranta;
            _caleAtac = caleAtac;
        }
    }
}
