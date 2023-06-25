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
                    data= element.GetAttribute("href").ToString();
                    
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
            
            foreach(var element in driver.FindElements(By.ClassName("ms-vb")))
            {
                //pageSource = pageSource + element.Text + "\n";
                //Console.WriteLine(pageSource);
                if(element.Text.Contains("Data") || element.Text.Contains("unic"))
                {
                    pageSource += element.Text + "\t";
                }
                else
                {
                    pageSource = pageSource + element.Text + "\n";
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
    }
}
