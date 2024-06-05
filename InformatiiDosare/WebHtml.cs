using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace InformatiiDosare
{
    internal class WebHtml
    {
        public string GetLinkDosar(string nrDosar)
        {
            string uriDosar = "";
            var html = new HtmlWeb();
            var htmlDoc = html.Load(nrDosar);
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//a"))
            {
                var titleName = node.GetAttributeValue("title", "");
                if (Tag.HasTitle(titleName))
                {
                    uriDosar = GetUriDosar(node);
                }
            }
            if (uriDosar.Length > 2)
            {
                uriDosar = "https://portal.just.ro" + uriDosar.Substring(2).Replace("&amp;", "&");
            }
            return uriDosar;
        }

        private string GetUriDosar(HtmlNode node)
        {
            return node.GetAttributeValue("href", "");
        }
        internal string GetAttributesTitle(string uri, string attribute)
        {
            string valueAt = "";
            var html = new HtmlWeb();
            var htmlDoc = html.Load(uri);
            foreach (var node in htmlDoc.DocumentNode.SelectNodes("//a"))
            {
                valueAt += node.GetAttributeValue(attribute, "");
                valueAt += " = ";
                foreach (char c in node.GetAttributeValue(attribute, "").ToCharArray())
                {
                    valueAt += ((byte)c).ToString() + ", ";
                }
                valueAt += Environment.NewLine;
            }
            return valueAt;
        }

        internal Dictionary<string, string> GetInformatiiDosar(string nrDosar, string uri)
        {
            Dictionary<string,string> infoDosar = new Dictionary<string,string>();
            if(String.IsNullOrWhiteSpace(uri) || String.IsNullOrEmpty(uri)) 
            {
                infoDosar.Add("informatiiGenerale", nrDosar + ",");
                infoDosar.Add("parti", nrDosar + ",");
                infoDosar.Add("sedinte", nrDosar + ",");
                infoDosar.Add("caiAtac", nrDosar + ",");
                return infoDosar;
            }
            HtmlWeb html = new HtmlWeb();
            HtmlDocument htmlDoc = html.Load(uri);
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//a"))
            {
                if (Tag.HasAttribute(node.GetAttributeValue("name", ""), Tag.INFORMATII_GENERALE))
                {
                    infoDosar.Add("informatiiGenerale", new HtmlTransform().TableInformatiiGenerale(nrDosar, node));
                }
                if(Tag.HasAttribute(node.GetAttributeValue("name",""), Tag.PARTI))
                {
                    infoDosar.Add("parti", new HtmlTransform().TableParti(nrDosar, node));
                }
                if(Tag.HasAttribute(node.GetAttributeValue("name",""), Tag.SEDINTE))
                {
                    infoDosar.Add("sedinte", new HtmlTransform().TableSedinte(nrDosar, node));
                }
                if(Tag.HasAttribute(node.GetAttributeValue("name",""), Tag.CAI_ATAC))
                {
                    infoDosar.Add("caiAtac", new HtmlTransform().TableCaiAtac(nrDosar, node));
                }
            }
            return infoDosar;
        }
    }
}
