using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class Parti
    {
        private List<Parte> _parti;
        public List<Parte> ListaParti { set { } get { return _parti; } }
        public Parti(HtmlAgilityPack.HtmlDocument htmlDocument)
        {
            _parti = new List<Parte>();
            foreach (Parte item in HtmlModel.TableParti(htmlDocument))
            {
                _parti.Add(item);
            }
        }
    }
}

