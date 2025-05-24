using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class CaiAtac
    {
        private List<CaleAtac> _recursuri;
        public CaiAtac(HtmlAgilityPack.HtmlDocument htmlDocument)
        {
            _recursuri = new List<CaleAtac>();
//            string[] detaliiRecurs;
            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//a"))
            {
                if (Utils.HasAttribute(node.GetAttributeValue("name", ""), Utils.CAI_ATAC))
                {
                    foreach(CaleAtac item in HtmlModel.TableCaiAtac(htmlDocument))
                    {
//                        if (item.Length > 0) 
//                        {
//                            detaliiRecurs = item.Split([Utils.CSV_DELIMITATOR]);
                            _recursuri.Add(item);
//                            _recursuri.Add(new CaleAtac(detaliiRecurs[0].Trim(),
//                                detaliiRecurs[1].Trim(), 
//                                detaliiRecurs[2].Trim()));
//                        }
                    }
                }
            }
        }
        public List<CaleAtac> ListaCaiAtac { set { _recursuri = value; } get { return _recursuri; } }
        //public CaiAtac AddCaleAtac(CaleAtac caleAtac)
        //{
        //    _recursuri.Add(caleAtac);
        //    return this;
        //}
    }
}
