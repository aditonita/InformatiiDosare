using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class Dosar
    {
        private string _nrDosar;
        private Instante _instante;
        public string NrDosar { set { _nrDosar = value; }  get { return _nrDosar; } }
        public Instante Instante { set { _instante = value; } get { return _instante; } }
        public Dosar(string nrDosar, Instante instante)
        {
            _nrDosar = nrDosar.Trim();
            _instante = instante;
        }
    }
}
