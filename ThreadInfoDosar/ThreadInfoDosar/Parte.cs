using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class Parte
    {
        private string _nume;
        private string _calitateParte;
        public string Nume { set { } get { return _nume; } }
        public string CalitateParte { set { } get { return _calitateParte; } }
        public Parte(string nume, string calitateParte)
        {
            _nume = nume;
            _calitateParte = calitateParte;
        }

    }
}
