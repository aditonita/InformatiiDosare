using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class Parti
    {
        private List<Parte> _parti;
        public List<Parte> ParteList { get { return _parti; } }
        public Parti() { _parti = new List<Parte>(); }
        public void AddParte(Parte parte) 
        {
          _parti.Add(parte);
        }
    }
}
