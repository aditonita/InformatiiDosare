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
        private void AddDosar(Dosar dosar)
        {
            _dosare.Add(dosar);
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
                    DateTime interval = today.AddDays(7);
                    return dosar.Instante.MaxDateInstanta() < new DateOnly(interval.Year, interval.Month, interval.Day) &&
                           dosar.Instante.MaxDateInstanta() >= new DateOnly(today.Year, today.Month, today.Day);
                }
                );
        }
        internal void Start()
        {
            int contor = 0;
            if (!File.Exists(Utils.INPUT_FILE))
            {
                string exMessage = "[ERROR] - Fisierul " + Utils.INPUT_FILE + " nu exista. " +
                       "Creati fisierul inainte de a rula aplicatia. " +
                       "Fisierul contine pe fiecare line un numar dosar. \n" +
                       "    - pentru ajutor rulati: InformatiiDosare.exe --help";
                throw new Exception(exMessage);
            }
            foreach (string nrDosar in IOControler.GetDosarNumbers())
            {
                string uriDosar = SetUri.PortalURI(nrDosar);
                List<string> linksDosar = WebControler.GetInstanteUri(uriDosar);
                Instante instante = new Instante();
                foreach (string linkDosar in linksDosar)
                {
                    IOControler.SaveUriDosare(uriDosar, linkDosar, Utils.URI_FILE, Utils.CSV_DELIMITATOR);
                    instante.AddInstanta(new Instanta(linkDosar));
                }
                AddDosar(new Dosar(nrDosar, instante));
                Utils.Progress(IOControler.GetDosarNumbers().Length, contor);
                contor++;
            }
            IOControler.SaveDetaliiDosare(this.GetOrderDosarList());
            IOControler.DosareInLucru(this.WorkforToday());
        }
    }
}
