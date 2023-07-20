using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class HtmlTransform
    {
        internal string TableInformatiiGenerale(string nrDosar, HtmlNode node)
        {
            string value = nrDosar + ",";
            if (node.ParentNode.NextSibling.NextSibling.
                    SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table") == null)
            {
                return "";
            }
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                    SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr/td[2]"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[2]/td[2]"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[3]/td[2]"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[4]/td[2]"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[5]/td[2]"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[6]/td[2]"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[7]/td[2]"));
            return value;
        }

        internal string TableParti(string nrDosar, HtmlNode node)
        {
            string value = nrDosar + ",";
            if (node.ParentNode.NextSibling.NextSibling.
                    SelectSingleNode("tr/td/div/table/tr/td/table") == null)
            {
                return "";
            }
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[2]/td"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[2]/td[2]"));
            value = value + Environment.NewLine + nrDosar + ",";
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[3]/td"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[3]/td[2]"));
            value = value + Environment.NewLine + nrDosar + ",";
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[4]/td"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[4]/td[2]"));
            value = value + Environment.NewLine + nrDosar + ",";
            return value;
        }
        internal string TableSedinte(string nrDosar, HtmlNode node)
        {
            string value = nrDosar + ",";
            if (node.ParentNode.NextSibling.NextSibling.
                    SelectSingleNode("tr/td/div/table/tr/td/table") == null)
            {
                return "";
            }
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr/td"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[2]/td"));
            value = value.Replace("Ora estimata:", "\"").Replace("Complet:", ",\"").Replace("Tip solutie:", "\",\"").
                Replace("Solutia pe scurt:", "\",\"").Replace("Document:","\",\"").Replace(Environment.NewLine,"").
                Replace("&nbsp; "," ").Replace("\t", "");
            return value;
        }
        internal string TableCaiAtac(string nrDosar, HtmlNode node)
        {
            string value = nrDosar + ",";
            if (node.ParentNode.NextSibling.NextSibling.
                    SelectSingleNode("tr/td/div/table/tr/td/table") == null)
            {
                return "";
            }
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[2]/td"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[2]/td[2]"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[2]/td[3]"));
            value = value + Environment.NewLine + nrDosar + ",";
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[3]/td"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[3]/td[2]"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[3]/td[3]"));
            value = value + Environment.NewLine + nrDosar + ",";
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[4]/td"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[4]/td[2]"));
            value = CurrentNodeValue(value, node.ParentNode.NextSibling.NextSibling.
                SelectSingleNode("tr/td/div/table/tr/td/table/tr[4]/td[3]"));
            value = value + Environment.NewLine + nrDosar + ",";
            return value;
        }
        private string CurrentNodeValue(string value, HtmlNode node)
        {
            if (node != null)
            {
                return value + "\"" + node.InnerText + "\"" + ",";
            }
            return value + ",";
        }
    }
}
