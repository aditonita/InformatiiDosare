using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class Sedinte
    {
        private List<Sedinta> _sedinte;
        public List<Sedinta> ListaSedinte { set { _sedinte = value; } get { return _sedinte; } }
        public Sedinte(HtmlAgilityPack.HtmlDocument htmlDocument)
        {
            _sedinte = new List<Sedinta>();
            foreach (Sedinta item in HtmlModel.TableSedinte(htmlDocument))
            {
//                if (item.Length > 0)
//                {
//                    detaliiSedinta = item.Split(new char[] { Utils.CSV_DELIMITATOR }, StringSplitOptions.None);
                    _sedinte.Add(item);
//                }
            }
        }
                   
        //public Sedinte AddSedinta(Sedinta sedinta)
        //{
        //    _sedinte.Add(sedinta);
        //    return this;
        //}
        public DateOnly MaxDateSession()
        {
            _sedinte.Sort((x, y) => y.StandardDate.CompareTo(x.StandardDate));
            return _sedinte[0].StandardDate;
        }
        public Sedinta SedintaByMaxDate()
        {
            _sedinte.Sort((x, y) => y.StandardDate.CompareTo(x.StandardDate));
            return _sedinte[0];
        }
    }
}
