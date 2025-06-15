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
using System.Security.Policy;

namespace InformatiiDosare
{
    internal class WebControler
    {
        /// <summary>
        /// Get all Instante URIs per NrDosar
        /// </summary>
        /// <param name="nrDosar"></param>
        /// <returns>Lista de URIs IdInstanta&IdDosar</returns>
        public static List<string> GetInstanteUri(string nrDosar)
        {
            string uriDosar;
            List<string> instanteUri = new List<string>();
            try
            {
                Task<HtmlAgilityPack.HtmlDocument> htmlDoc = GetHtml(nrDosar);
                foreach (HtmlNode node in htmlDoc.Result.DocumentNode.SelectNodes("//a"))
                {
                    var titleName = node.GetAttributeValue("title", "");
                    if (Utils.HasTitle(titleName))
                    {
                        uriDosar = GetUriDosar(node);
                        if (uriDosar.Length > 2)
                        {
                            uriDosar = "https://portal.just.ro" + uriDosar.Substring(2).Replace("&amp;", "&");
                        }
                        instanteUri.Add(uriDosar);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]-{0};{1}",nrDosar,ex);
                Console.WriteLine(ex.Message);
            }
            return instanteUri;
        }
        private static string GetUriDosar(HtmlNode node)
        {
            return node.GetAttributeValue("href", "");
        }
        internal static string GetAttributesTitle(string uri, string attribute)
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
        /// <summary>
        /// get html page for uri link
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>htmlDocument</returns>
        internal static async Task<HtmlAgilityPack.HtmlDocument>  GetHtml(string uri)
        {
            HtmlWeb html = new HtmlWeb();
            html.Timeout = Utils.HTML_TIMEOUT;
            return await html.LoadFromWebAsync(uri);
            //return html.Load(uri);
        }
    }
}
