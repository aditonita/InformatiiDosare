using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InformatiiDosare
{
    internal class HtmlModel
    {
        /// <summary>
        /// get detalii instanta by Dosar
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <returns>NrUnic, DataInregistrarii, DataUltimeiModificari, Sectie, Materie, Obiect, Stadiu procesual</returns>
        internal static string[] TableInformatiiGenerale(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            string[] value = new string[7];
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//a"))
            {
                if (Utils.HasAttribute(node.GetAttributeValue("name", ""), Utils.INFORMATII_GENERALE))
                {
                    if (node.ParentNode.NextSibling.NextSibling.
        SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table") != null)
                    {
                        value[0] = CurrentNodeValue(node.ParentNode.NextSibling.NextSibling.
                                SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr/td[2]"));
                        value[1] = CurrentNodeValue(node.ParentNode.NextSibling.NextSibling.
                            SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[2]/td[2]"));
                        value[2] = CurrentNodeValue(node.ParentNode.NextSibling.NextSibling.
                            SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[3]/td[2]"));
                        value[3] = CurrentNodeValue(node.ParentNode.NextSibling.NextSibling.
                            SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[4]/td[2]"));
                        value[4] = CurrentNodeValue(node.ParentNode.NextSibling.NextSibling.
                            SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[5]/td[2]"));
                        value[5] = CurrentNodeValue(node.ParentNode.NextSibling.NextSibling.
                            SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[6]/td[2]"));
                        value[6] = CurrentNodeValue(node.ParentNode.NextSibling.NextSibling.
                            SelectSingleNode("tr/td/div/table/tr/td/table/tr/td/table/tr[7]/td[2]"));
                    }
                }
            }
            return value;
        }
        /// <summary>
        /// get parti by Dosar
        /// </summary>
        /// <param name="nrDosar"></param>
        /// <param name="node"></param>
        /// <returns>Nume, Calitate parte</returns>
        internal static Parte[] TableParti(HtmlAgilityPack.HtmlDocument htmlDocument)
        {
            Parte[] clienti = new Parte[0];
            int i = 2;
            bool hasNextNode = true;
            HtmlNode nextNode;
            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//a"))
            {
                if (Utils.HasAttribute(node.GetAttributeValue("name", ""), Utils.PARTI))
                {
                    if (node.ParentNode.NextSibling.NextSibling.
                            SelectSingleNode("tr/td/div/table/tr/td/table/tr[" + i.ToString() + "]") == null)
                    {
                        Array.Resize(ref clienti, clienti.Length + 1);
                        clienti[0] = new Parte(ConvertSpecialCharsToAscii(node.ParentNode.NextSibling
                            .NextSibling.SelectSingleNode("tr/td/div/table/tr/td/table").InnerText), "");
                        return clienti;
                    }
                        while (hasNextNode)
                    {
                        int k = clienti.Length;
                        Array.Resize(ref clienti, k + 1);
                        clienti[k] = new Parte(CurrentNodeValue(node.ParentNode.NextSibling.NextSibling
                                .SelectSingleNode("tr/td/div/table/tr/td/table/tr[" + i.ToString() + "]/td")),
                            CurrentNodeValue(node.ParentNode.NextSibling.NextSibling
                                .SelectSingleNode("tr/td/div/table/tr/td/table/tr[" + i.ToString() + "]/td[2]")));
                        nextNode = node.ParentNode.NextSibling.NextSibling
                            .SelectNodes("tr/td/div/table/tr/td/table/tr[" + i.ToString() + "]")
                            .ElementAt(0).NextSibling;
                        hasNextNode = (nextNode != null) && hasNextNode;
                        i++;
                    }
                }
            }
            return clienti;
        }
        /// <summary>
        /// Get detalii sedinte by Dosar
        /// </summary>
        /// <param name=""></param>
        /// <returns>Data, Ora estimata, Complet, Tip solutie, Solutia pe scurt, Document</returns>
        internal static Sedinta[] TableSedinte(HtmlAgilityPack.HtmlDocument htmlDocument)
        {
            Sedinta[] lawsuit = new Sedinta[0];
            int i = 2;
            int j = 1;
            bool hasNextNode = true;
            HtmlNode nextNode;
            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//a"))
            {
                if (Utils.HasAttribute(node.GetAttributeValue("name", ""), Utils.SEDINTE))
                {
                    if (node.ParentNode.NextSibling.NextSibling
                        .SelectSingleNode("tr/td/div/table/tr/td/table/tr/td")
                        .GetDirectInnerText().Length > 0)
                    {
                        Array.Resize(ref lawsuit, lawsuit.Length + 1);
                        lawsuit[0] = new Sedinta("", "", ConvertSpecialCharsToAscii(node.ParentNode
                            .NextSibling.NextSibling.SelectSingleNode("tr/td/div/table/tr/td/table/tr/td")
                        .GetDirectInnerText()), "", "", "");
                        return lawsuit;
                    }
                    while (hasNextNode)
                    {
                        int k = lawsuit.Length;
                        Array.Resize(ref lawsuit, k + 1);
                        string[] detaliiSedinta = CurrentNodeValue( node.ParentNode.NextSibling.NextSibling
                            .SelectSingleNode("tr/td/div/table/tr/td/table/tr[" + i.ToString() + "]/td"))
                                .Replace("Ora estimata:", "").Replace("Complet:", "#")
                            .Replace("Tip solutie:", "#")
                            .Replace("Solutia pe scurt:", "#")
                            .Replace("Document:", "#")
                            .Replace(Environment.NewLine, "")
                            .Replace("&nbsp; ", " ").Replace("\t", "")
                            .Split(new string[] {"#"}, StringSplitOptions.None);
                        lawsuit[k] = new Sedinta(
                            CurrentNodeValue(node.ParentNode.NextSibling.NextSibling.
                            SelectSingleNode("tr/td/div/table/tr/td/table/tr[" + j.ToString() + "]/td")),
                            detaliiSedinta[0], detaliiSedinta[1], detaliiSedinta[2], detaliiSedinta[3],
                            detaliiSedinta[4]);
                        nextNode = node.ParentNode.NextSibling.NextSibling
                            .SelectNodes("tr/td/div/table/tr/td/table/tr[" + i.ToString() + "]").ElementAt(0).NextSibling;
                        hasNextNode = (nextNode != null) && hasNextNode;
                        nextNode = node.ParentNode.NextSibling.NextSibling
                            .SelectNodes("tr/td/div/table/tr/td/table/tr[" + j.ToString() + "]").ElementAt(0).NextSibling;
                        hasNextNode = (nextNode != null) && hasNextNode;
                        i = i + 2;
                        j = j + 2;
                    }
                }
            }
            return lawsuit;
        }
        /// <summary>
        /// get recurs by Dosar
        /// </summary>
        /// <param name="htmlDocument"></param>
        /// <returns>Data declarare, Parte declaranta, Cale de atac</returns>
        internal static CaleAtac[] TableCaiAtac(HtmlAgilityPack.HtmlDocument htmlDocument)
        {
            int i = 2;
            bool hasNexNode = true;
            HtmlNode nextNode;
            CaleAtac[] recurs = new CaleAtac[0];
            //string value = "";
            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//a"))
            {
                if (Utils.HasAttribute(node.GetAttributeValue("name", ""), Utils.CAI_ATAC))
                {
                    if (node.ParentNode.NextSibling.NextSibling.SelectSingleNode("tr/td/div/table/tr/td/table/tr[" + i.ToString() + "]") == null)
                    {
                        Array.Resize(ref recurs, recurs.Length + 1);
                        recurs[0] = new CaleAtac("",
                            ConvertSpecialCharsToAscii(node.ParentNode.NextSibling.NextSibling
                            .SelectSingleNode("tr/td/div/table/tr/td/table").InnerText), "");
                        return recurs;
                    }
                    while (hasNexNode)
                    {
                        int k = recurs.Length;
                        Array.Resize(ref recurs, k + 1);
                        recurs[k] = new CaleAtac(
                              CurrentNodeValue(node.ParentNode.NextSibling.NextSibling 
                                .SelectSingleNode("tr/td/div/table/tr/td/table/tr[" + i.ToString() + "]/td")),
                              CurrentNodeValue(node.ParentNode.NextSibling.NextSibling
                                .SelectSingleNode("tr/td/div/table/tr/td/table/tr[" + i.ToString() + "]/td[2]")),
                              CurrentNodeValue(node.ParentNode.NextSibling.NextSibling
                                .SelectSingleNode("tr/td/div/table/tr/td/table/tr[" + i.ToString() + "]/td[3]"))
                              );
                        nextNode = node.ParentNode.NextSibling.NextSibling
                            .SelectNodes("tr/td/div/table/tr/td/table/tr[" + i.ToString() + "]").ElementAt(0).NextSibling;
                        hasNexNode = (nextNode != null) && hasNexNode;
                        i++;
                    }
                }
            }
            return recurs;
        }
        private static string CurrentNodeValue(HtmlNode node)
        {
            if (node != null)
            {
                return ConvertSpecialCharsToAscii(node.InnerText.Trim());
            }
            return "";
        }
        private static string ConvertSpecialCharsToAscii(string romanianChars)
        {
            string result = "";
            result = romanianChars.Replace('ă', 'a').Replace('â', 'a').
                Replace('Ă', 'A').Replace('Â', 'A').Replace('î', 'i').Replace('Î', 'I').
                Replace('ș', 's').Replace('ş', 's').Replace('Ș', 'S').Replace('Ş', 'S').
                Replace('ț', 't').Replace('ţ', 't').Replace('Ț', 'T').Replace('Ţ', 'T');
            return result;
        }
        /// <summary>
        /// get instanta by dosar
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <returns>string NumeInstanta</returns>
        internal static string NumeInstanta(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            string value = "";
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//a"))
            {
                if (Utils.HasAttribute(node.GetAttributeValue("title", ""), Utils.PAGINA_PRINCIPALA))
                {
                    if (!(string.IsNullOrEmpty(node.ParentNode.InnerText)))
                    {
                        value = Regex.Replace(CurrentNodeValue(node.ParentNode), @"\t|\n|\r", "");
                    }
                }
            }
            return value;
        }
    }
}
