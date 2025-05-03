using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class Dosar
    {
        private String _nrDosar;
        private String _instanta;
        private String _uriDosar;
        private DateOnly _dataInregistrare;
        private DateOnly _dataUltimaModificare;
        private String _sectie;
        private String _materie;
        private String _obiect;
        private String _stadiuProcesual;
        private CaiAtac _caiAtac;
        private Parti _parti;
        private Sedinte _sedinte;

        public String GetNrDosar { get { return _nrDosar; } }
        public String GetIstanta { get { return _instanta; } }
        public String GetUriDosar => _uriDosar;
        public DateOnly GetDataInregistrare => _dataInregistrare;
        public DateOnly GetDataUltimaModificare => _dataUltimaModificare;
        public String GetSectie { get { return _sectie; } }
        public String GetMaterie => _materie;
        public String GetObiect { get { return _obiect; } }
        public String GetStadiuProcesual => _stadiuProcesual;
        public CaiAtac GetCaiAtac => _caiAtac;
        public Parti GetParti => _parti;
        public Sedinte GetSedinte { get { return _sedinte; } }

        public Dosar(string nrDosar, string instanta, string uriDosar, string dataInregistrare, string dataUltimaModificare, string sectie, string materie, string obiect, string stadiuProcesual, CaiAtac caiAtac, Parti parti, Sedinte sedinte)
        {
            nrDosar = nrDosar.Trim();
            instanta = instanta.Trim();
            uriDosar = uriDosar.Trim();
            dataInregistrare = dataInregistrare.Trim();
            dataUltimaModificare = dataUltimaModificare.Trim();
            sectie = sectie.Trim();
            materie = materie.Trim(); 
            obiect = obiect.Trim();
            stadiuProcesual = stadiuProcesual.Trim();
            _nrDosar = nrDosar;
            _instanta = instanta;
            _uriDosar = uriDosar;
            _dataInregistrare = new DateOnly(Int32.Parse(dataInregistrare.Substring(6, 4)), Int32.Parse(dataInregistrare.Substring(3, 2)), Int32.Parse(dataInregistrare.Substring(0, 2)));
            _dataUltimaModificare = new DateOnly(Int32.Parse(dataUltimaModificare.Substring(6, 4)), Int32.Parse(dataUltimaModificare.Substring(3, 2)), Int32.Parse(dataUltimaModificare.Substring(0, 2)));
            _sectie = sectie;
            _materie = materie;
            _obiect = obiect;
            _stadiuProcesual = stadiuProcesual;
            _caiAtac = caiAtac;
            _parti = parti;
            _sedinte = sedinte;
        }
    }

}
