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
        private string _complet;
        private string _tipSolutie;
        private string _solutiePeScurt;
        private string _document;
        public DateOnly StandardDate { set { _standardDate = value; } get { return _standardDate; } }
        public TimeOnly OraEstimata { set { _oraEstimata = value; } get { return _oraEstimata;} }
        public string Complet { set { _complet = value; } get { return _complet; } }
        public string TipSolutie { set { _tipSolutie = value; } get { return _tipSolutie; } }
        public string SolutiePeScurt { set { _solutiePeScurt = value; } get { return _solutiePeScurt; }  }
        public string Document { set { _document = value; } get { return _document; } }
        public Sedinta(string standardDate, string oraEstimata, string complet, string tipSolutie, string solutiePeScurt, string document)
        {
            standardDate = standardDate.Trim();
            oraEstimata = oraEstimata.Trim();
            complet = complet.Trim();
            tipSolutie = tipSolutie.Trim();
            solutiePeScurt = solutiePeScurt.Trim();
            document = document.Trim();
            if (Utils.IsDateFormat(standardDate)) 
            {
                string[] date = standardDate.Split(['.']);
                _standardDate = new DateOnly(Int32.Parse(date[2].Trim()), 
                    Int32.Parse(date[1].Trim()), Int32.Parse(date[0].Trim()));
                string[] time = oraEstimata.Split([':']);
                _oraEstimata = new TimeOnly(Int32.Parse(time[0].Trim()), Int32.Parse(time[1].Trim()));
            }
            else
            {
                _standardDate = new DateOnly();
                _oraEstimata = new TimeOnly();
            }
            _complet = complet;
            _tipSolutie = tipSolutie;
            _solutiePeScurt = solutiePeScurt;
            _document = document;
        }
    }
}
