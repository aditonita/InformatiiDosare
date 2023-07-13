using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace InformatiiDosare
{
    internal class WebHtml
    {
        //        public String HtmlPage { set; get; }
        //        public WebHtml(string uri)
        //        {
        //            HtmlPage = GetDosarUri(uri);
        //        }
        //#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        //        public WebHtml()
        //#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        //        {
        //            
        //        }

        public void GetLinkDosar(string nrDosar) 
        {
            var html = new HtmlWeb();
            var htmlDoc = html.Load(nrDosar);
            foreach (var node in htmlDoc.DocumentNode.SelectNodes("//a"))
                {
                var test = node.GetAttributeValue("title", "");
                Console.WriteLine(test);
                Console.WriteLine(test.Equals( "Informatii dosar"));
                Console.WriteLine(node.GetAttributeValue("href",""));
                //../299/SitePages/Dosar.aspx?id_dosar=29900000000992959&amp;id_inst=299
                // "https://portal.just.ro/299/SitePages/Dosar.aspx?id_dosar=29900000000992959&id_inst=299"
            }
        }
        public string GetDosarUri(string uri)
        {
            string data = "";
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;
            var driver = new ChromeDriver(service, options);
            driver.Url = uri;
            //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(90);
            //.ImplicitWait = TimeSpan.FromSeconds(30);

            var elements = driver.FindElements(By.XPath("//a"));
            foreach (var element in elements)
            {
                if ((element.GetAttribute("title").ToLower().Contains("ii dosar")))
                {

                    data = element.GetAttribute("href").ToString();

                    
                    //73,110,102,111,114,109,97,99,105,105,32,100,111,115,97,114
                    
                    Console.WriteLine(element.GetAttribute("title"));
                    foreach(char c in element.GetAttribute("title"))
                    {
                        Console.Write((byte)c);
                        Console.WriteLine("\t");
                    }
                    Console.WriteLine("");
                    data = element.GetAttribute("href").ToString();
                    
                }
            }
            driver.Close();
            driver.Quit();
            driver.Dispose();
            return data;
        }

        public string GetDosarData(string uri)
        {
            string data = "";
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;
            var driver = new ChromeDriver(service, options);

            //var driver = new ChromeDriver();
            driver.Url = uri;
            string pageSource = "";

            foreach (var element in driver.FindElements(By.TagName("a")))
            {
                //pageSource = pageSource + element.Text + "\n";

                string nameTag = element.GetAttribute("Name") ?? "";
                //Console.WriteLine(  nameTag + nameTag.Length.ToString());

                //******
                //int[] sedinte = new int[] { 95, 101, 100, 105, 110, 99, 101 };
                //if (nameTag != "" && nameTag.Length == sedinte.Length)
                //if (nameTag != "" && nameTag.Length == 8)
                //{
                //StringBuilder sedinte = new StringBuilder().Append('S').Append((char)101).Append((char)100).Append((char)105).Append((char)110).Append('t').Append((char)101);
                //Console.WriteLine(new StringBuilder(nameTag).Equals(sedinte));
                //Console.WriteLine(nameTag);
                //Console.WriteLine(  new StringBuilder(nameTag).ToString());

                //for (int i = 0; i < nameTag.Length;i++) 
                //{
                //     Console.WriteLine((byte)nameTag.ToLower().ToCharArray()[i]);
                //     Console.WriteLine((byte)nameTag.ToLower().ToCharArray()[i] == sedinte[i]);
                //Console.WriteLine((byte)nameTag.ToCharArray()[i]);

                //}


                //Console.WriteLine((byte)nameTag.ToLower().ToCharArray()[0]);
                //Console.WriteLine((byte)nameTag.ToLower().ToCharArray()[1]);
                //Console.WriteLine((byte)nameTag.ToLower().ToCharArray()[2]);
                //Console.WriteLine((byte)nameTag.ToLower().ToCharArray()[3]);
                //Console.WriteLine((byte)nameTag.ToLower().ToCharArray()[4]);
                //Console.WriteLine((byte)nameTag.ToLower().ToCharArray()[5]);
                //Console.WriteLine((byte)nameTag.ToLower().ToCharArray()[6]);

                //Console.WriteLine((byte)nameTag.ToLower().ToCharArray()[0] == 95);
                //}



                //if(element.Text.Contains("Data") || element.Text.Contains("unic"))
                //if (element.GetAttribute("name").ToLower().Contains("edin"))
                //Console.WriteLine(Tag.IsTagName(nameTag));
                if (Tag.IsTagName(nameTag, TagName.TAG_NAME_SEDINTE))
                {
                    Encoding.ASCII.GetBytes(nameTag).ToList().ForEach(s => Console.Write(s + "\t"));
                    Console.WriteLine();
                    Encoding.UTF8.GetBytes(nameTag).ToList().ForEach(s => Console.Write(s + "\t"));
                    Console.WriteLine();
                    Encoding.Unicode.GetBytes(nameTag).ToList().ForEach(s => Console.Write(s + "\t"));
                    Console.WriteLine();
                    Encoding.ASCII.GetBytes("Sedinte").ToList().ForEach(s => Console.Write(s + "\t"));
                    Console.WriteLine();
                    Encoding.UTF8.GetBytes("Sedinte").ToList().ForEach(s => Console.Write(s + "\t"));
                    Console.WriteLine();
                    Encoding.Unicode.GetBytes("Sedinte").ToList().ForEach(s => Console.Write(s + "\t"));
                    Console.WriteLine();


                    pageSource += element.Text + "\n";

                    pageSource += element.FindElement(By.XPath("//parent::h3")).FindElement(By.XPath("//parent::div")).FindElement(By.XPath("//table[3]/tbody/tr/td/div/div/table/tbody/tr/td/b/a")).Text + "\n";
                    pageSource += element.FindElement(By.XPath("//parent::h3")).FindElement(By.XPath("//parent::div")).FindElement(By.XPath("//table[3]/tbody/tr/td/div/div/table/tbody/tr[2]/td")).Text + "\n";

                }
                else
                {
                    //pageSource = pageSource + element.Text + "\n";
                }
            }
            pageSource += "\n" + "============================================";
            //String pageSource = driver.PageSource.ToString();
            data = pageSource;
            driver.Close();
            driver.Quit();
            driver.Dispose();
            return data;
        }

        public static void GetTagsByByte(string uri)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;
            var driver = new ChromeDriver(service, options);

            driver.Url = uri;

            foreach (var element in driver.FindElements(By.TagName("a")))
            {
                if (element.GetAttribute("name") != null || element.GetAttribute("name") != "")
                {
                    Console.Write(element.GetAttribute("name="));
                    foreach (char c in element.GetAttribute("name").ToCharArray())
                    {
                        Console.Write(c);
                        Console.Write(',');
                    }
                    Console.WriteLine("======================================================");
                }
            }
        }
    }
}
