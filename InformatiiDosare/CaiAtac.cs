using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class CaiAtac
    {
        private List<CaleAtac> _caiatac;
        public CaiAtac()
        {
            _caiatac = new List<CaleAtac>();
        }
        public void AddCaleAtac(CaleAtac caleAtac) { _caiatac.Add(caleAtac); }
    }
}
