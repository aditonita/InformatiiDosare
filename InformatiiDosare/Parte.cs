using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class Parte
    {
        private String _nume;
        private String _calitateParte;
        public String GetNume { get { return _nume; } }
        public String GetCalitateParte { get {  return _calitateParte; } } 
        public Parte(String nume, String calitateParte)
        {
            _nume = nume;
            _calitateParte = calitateParte;
        }

    }
}
