using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class Sedinte
    {
        private List<Sedinta> _sedinte;
        public List<Sedinta> SedintaList => _sedinte;
        public Sedinte()
        {
            _sedinte = new List<Sedinta>();
        }
        
        public void AddSedinta(Sedinta sedinta)
        {
            _sedinte.Add(sedinta);
        }

        public DateOnly MaxDateSession()
        {
            _sedinte.Sort((x, y) => y.GetStandardDate.CompareTo(x.GetStandardDate));
            return _sedinte[0].GetStandardDate;
        }
        
    }
}
