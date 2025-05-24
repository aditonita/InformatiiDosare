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
            //string[] detaliiParte;
            foreach (Parte item in HtmlModel.TableParti(htmlDocument))
            {
                //                if (item.Length > 0)
                //                {
                //                    detaliiParte = item.Split(new char[] { Utils.CSV_DELIMITATOR });
                _parti.Add(item);
                //_parti.Add(new Parte(detaliiParte[0], detaliiParte[1]));

            }
        }
    }
    //public Parti AddParte(Parte parte)
    //{
    //    _parti.Add(parte);
    //    return this;
    //}
}

