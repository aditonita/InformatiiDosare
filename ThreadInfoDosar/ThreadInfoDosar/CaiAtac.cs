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
            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//a"))
            {
                if (Utils.HasAttribute(node.GetAttributeValue("name", ""), Utils.CAI_ATAC))
                {
                    foreach(CaleAtac item in HtmlModel.TableCaiAtac(htmlDocument))
                    {
                            _recursuri.Add(item);
                    }
                }
            }
        }
        public List<CaleAtac> ListaCaiAtac { set { _recursuri = value; } get { return _recursuri; } }
    }
}
