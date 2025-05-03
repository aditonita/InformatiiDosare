using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class Sedinta
    {
        private DateOnly _standardDate;
        private TimeOnly _oraEstimata;
        private String _complet;
        private String _tipSolutie;
        private String _solutiePeScurt;
        private String _document;
        public DateOnly GetStandardDate => _standardDate; 
        public TimeOnly GetOraEstimata => _oraEstimata;
        public String GetComplet => _complet;
        public String GetTipSolutie { get { return _tipSolutie; } }
        public String GetSolutiePeScurt => _solutiePeScurt;
        public String GetDocument => _document;

        public Sedinta(String standardDate, String oraEstimata, String complet, String tipSolutie, String solutiePeScurt, String document)
        {
            standardDate = standardDate.Trim();
            oraEstimata = oraEstimata.Trim();
            complet = complet.Trim();
            tipSolutie = tipSolutie.Trim();
            solutiePeScurt = solutiePeScurt.Trim();
            document = document.Trim();
            _standardDate = new DateOnly(Int32.Parse(standardDate.Substring(0, 4)), Int32.Parse(standardDate.Substring(5, 2)), Int32.Parse(standardDate.Substring(8, 2)));
            _oraEstimata = new TimeOnly(Int32.Parse(oraEstimata.Substring(0, 2)), Int32.Parse(oraEstimata.Substring(3, 2)));
            _complet = complet;
            _tipSolutie = tipSolutie;
            _solutiePeScurt = solutiePeScurt;
            _document = document;
        }


    }
}
