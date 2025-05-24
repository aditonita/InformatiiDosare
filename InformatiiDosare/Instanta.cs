namespace InformatiiDosare
{
    internal class Instanta
    {
        #region parameters
        private string _numeInstanta;
        private string _nrUnic;
        private string _uriDosar;
        private DateOnly _dataInregistrare;
        private DateOnly _dataUltimaModificare;
        private string _sectie;
        private string _materie;
        private string _obiect;
        private string _stadiuProcesual;
        private CaiAtac _caiAtac;
        private Parti _parti;
        private Sedinte _sedinte;
        #endregion
        #region seter
        public string NumeIstanta { set { _numeInstanta = value; } get { return _numeInstanta; } }
        public string NrUnic { set { _nrUnic = value; } get { return _nrUnic; } }
        public string UriDosar { set { _uriDosar = value; } get { return _uriDosar; } }
        public DateOnly DataInregistrare { set { _dataInregistrare = value; } get { return _dataInregistrare; } }
        public DateOnly DataUltimaModificare { set { _dataUltimaModificare = value; } get { return _dataUltimaModificare; } }
        public string Sectie { set { _sectie = value; } get { return _sectie; } }
        public string Materie { set { _materie = value; } get { return _materie; } }
        public string Obiect { set { _obiect = value; } get { return _obiect; } }
        public string StadiuProcesual { set { _stadiuProcesual = value; } get { return _stadiuProcesual; } }
        public CaiAtac CaiAtac { set { _caiAtac = value; } get { return _caiAtac; } }
        public Parti Parti { set { _parti = value; } get { return _parti; } }
        public Sedinte Sedinte { set { _sedinte = value; } get { return _sedinte; } }
        #endregion
        public Instanta(string idInstantaUri)
        {
            HtmlAgilityPack.HtmlDocument htmlDocument = WebControler.GetHtml(idInstantaUri);
            string[] informatiiGenerale = HtmlModel.TableInformatiiGenerale(htmlDocument);
            _numeInstanta = HtmlModel.NumeInstanta(htmlDocument);
            _nrUnic = informatiiGenerale[0];
            _uriDosar = idInstantaUri;
            if (Utils.IsDateFormat(informatiiGenerale[1]))
            {
                string[] date = informatiiGenerale[1].Split(['.']);
                _dataInregistrare = new DateOnly(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));
                if (Utils.IsDateFormat(informatiiGenerale[2]))
                {
                    date = informatiiGenerale[2].Split(['.']);
                    _dataUltimaModificare = new DateOnly(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));
                    _sectie = informatiiGenerale[3];
                    _materie = informatiiGenerale[4];
                    _obiect = informatiiGenerale[5];
                    _stadiuProcesual = informatiiGenerale[6];
                }
            }
            else
            {
                _dataInregistrare = new DateOnly();
                _dataInregistrare = new DateOnly();
                _sectie = informatiiGenerale[1] + informatiiGenerale[2];
                _materie = String.Empty; _obiect = String.Empty; _stadiuProcesual = String.Empty;
            }
            _caiAtac = new CaiAtac(htmlDocument);
            _parti = new Parti(htmlDocument);
            _sedinte = new Sedinte(htmlDocument);
        }
    }
}