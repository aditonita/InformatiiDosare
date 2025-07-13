using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class CaleAtac
    {
        private DateOnly _dataDeclarare;
        private string _parteDeclaranta;
        private string _recurs;

        public DateOnly DataDeclarare { set { } get { return _dataDeclarare; } }
        public string ParteDeclaranta { set { } get { return _parteDeclaranta; } }
        public string Recurs { set { } get {return _recurs;} }
        public CaleAtac(string dataDeclarare, string parteDeclaranta, string recurs)
        {
            if(Utils.IsDateFormat(dataDeclarare))
            {
                string[] date = dataDeclarare.Trim().Split(new char[] { '.','/' }, StringSplitOptions.None);
                _dataDeclarare = new DateOnly(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));
                _parteDeclaranta = parteDeclaranta;
                _recurs = recurs;
            }
            else
            {
                _dataDeclarare = new DateOnly();
                _parteDeclaranta = parteDeclaranta;
                _recurs = String.Empty;
            }
        }
    }
}
