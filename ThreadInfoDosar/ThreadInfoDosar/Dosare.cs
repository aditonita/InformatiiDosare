using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class Dosare
    {
        private List<Dosar> _dosare;
        public List<Dosar> ListaDosare { set { _dosare = value; } get { return _dosare; } }
        public Dosare()
        {
            _dosare = new List<Dosar>();
        }

        private List<Dosar> GetOrderDosarList()
        {
            _dosare.Sort((x, y) => y.Instante.MaxDateInstanta().CompareTo(x.Instante.MaxDateInstanta()));
            return _dosare;
        }
        public List<Dosar> WorkforToday()
        {
            return _dosare.FindAll
                (
                delegate (Dosar dosar)
                {
                    DateTime today = DateTime.Now;
                    DateTime interval = today.AddDays(Utils.FORECAST_DAYS);
                    return dosar.Instante.MaxDateInstanta() < new DateOnly(interval.Year, interval.Month, interval.Day) &&
                           dosar.Instante.MaxDateInstanta() >= new DateOnly(today.Year, today.Month, today.Day);
                }
                );
        }
        internal void Start(string index, string[] nrDosare)
        {
            int contor = 0;
            foreach (string nrDosar in nrDosare)
            {
                string uriDosar = SetUri.PortalURI(nrDosar);
                List<string> linksDosar = WebControler.GetInstanteUri(uriDosar);
                Instante instante = new Instante();
                foreach (string linkDosar in linksDosar)
                {
                    IOControler.SaveUriDosare(uriDosar, linkDosar, Utils.URI_FILE + index, Utils.CSV_DELIMITATOR);
                    instante.AddInstanta(new Instanta(linkDosar));
                }
                Task.Run(() => _dosare.Add(new Dosar(nrDosar, instante)));
                //_dosare.Add(new Dosar(nrDosar, instante));
                Utils.Progress(nrDosare.Length, contor);
                contor++;
            }
            IOControler.SaveDetaliiDosare(index, this.GetOrderDosarList());
            IOControler.DosareInLucru(index, this.WorkforToday());
        }
    }
}
