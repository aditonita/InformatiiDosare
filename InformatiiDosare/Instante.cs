namespace InformatiiDosare
{
    internal class Instante
    {
        private List<Instanta> _instante;
        public List<Instanta> ListaInstante
        {
            set {  _instante = value; }
            get { return _instante; }
        }
        public Instante()
        {
            _instante = [];
        }
        /// <summary>
        /// Adauga o noua instanta la lista de instante
        /// </summary>
        /// <param name="instanta"></param>
        public void AddInstanta(Instanta instanta)
        {
            _instante.Add(instanta);
        }
        public DateOnly MaxDateInstanta()
        {
            _instante.Sort((x, y) => y.Sedinte.MaxDateSession().CompareTo(x.Sedinte.MaxDateSession()));
            if (_instante.Count > 0) 
            {
                return _instante[0].Sedinte.MaxDateSession();
            }
                return new DateOnly();
        }
        public Instanta InstantaByMaxDate()
        {
            _instante.Sort((x, y) => y.Sedinte.MaxDateSession().CompareTo(x.Sedinte.MaxDateSession()));
            if (_instante.Count > 0)
            {
                return _instante[0];
            }
            return new Instanta("");
        }
    }
}