using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class Dosare
    {
        private List<Dosar> _dosare;

        public List<Dosar> DosarList { get { return _dosare; } }

        public Dosare()
        {
            _dosare = new List<Dosar>();
        }

        public void AddDosar(Dosar dosar)
        {
            _dosare.Add(dosar);
        }

        public List<Dosar> GetOrderDosarList() {
            _dosare.Sort((x, y) => y.GetSedinte.MaxDateSession().CompareTo(x.GetSedinte.MaxDateSession()));
            return _dosare;
        }
    }
}
