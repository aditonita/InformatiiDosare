// See https://aka.ms/new-console-template for more information

using InformatiiDosare;
using OpenQA.Selenium.Remote;
//using Microsoft.Extensions.Configuration;
using System;


//IConfiguration config = new ConfigurationBuilder()
//    .AddJsonFile($"appsettings.json")
//    .AddEnvironmentVariables()
//    .Build();
//
//string sedinte = config["Sedinte"]??"";
//Console.WriteLine(sedinte);

//Tag._SEDINTE.ToList().ForEach(s => Console.Write(s + "\t"));
//Console.WriteLine();
Tag.SEDINTE.ToList().ForEach(s => Console.Write(s + "\t"));
//Console.ReadLine();
if (args.Length > 1)
{
    if (args[0].Equals("tags") || args[0].Equals('t'))
    {
        if (args[1].StartsWith("--dosar"))
        {
            Console.WriteLine(args[1].Split('=')[1]);
            string portalUri = new WebHtml().GetDosarUri(SetUri.PortalURI(args[1].Split('=')[1]));
            {
                WebHtml.GetTagsByByte(portalUri);
            }
        }
    }
    
}
else
{
    //SetUri setUri = new SetUri("https://portal.just.ro", "16944/215/2020");
    foreach (string line in IODosar.GetNrDosare("input"))
    {
        SetUri setUri = new SetUri("https://portal.just.ro", line);
        string uri = setUri.Uri.ToString();
        new WebHtml().GetLinkDosar(uri);
        //uri = new WebHtml().GetDosarUri(uri);
        //IODosar.SaveDosarData(new WebHtml().GetDosarData(uri));
      
char delim = ',';

//SetUri setUri = new SetUri("https://portal.just.ro", "16944/215/2020");
foreach(string line in IODosar.GetNrDosare("input"))
{
    Console.WriteLine(line);
    SetUri setUri = new SetUri("https://portal.just.ro", line);
    string uri = setUri.Uri.ToString();
    new WebHtml().GetLinkDosar(uri);
    //uri = new WebHtml().GetDosarUri(uri);
    //IODosar.SaveDosarData(new WebHtml().GetDosarData(uri));
    //uri = new WebHtml().GetDosarUri(uri);
    IODosar.SaveDosarData(line + delim + new WebHtml().GetDosarUri(uri),"URI_dosare");
    //IODosar.SaveDosarData(new WebHtml().GetDosarData(uri), "output");


}

foreach (string dosarUri in IODosar.GetDosarURIs("URI_dosare", delim))
{
    Console.WriteLine(dosarUri);
    ////IODosar.SaveDosarData(new WebHtml().GetDosarData(dosarUri), "output");
}

//string uri = setUri.Uri.ToString();

      IODosar.SaveDosarData(line);
        //SetUri setUri = new SetUri("https://portal.just.ro", line);
        //string uri = setUri.Uri.ToString();
        uri = new WebHtml().GetDosarUri(uri);
        IODosar.SaveDosarData(new WebHtml().GetDosarData(uri));
    }
    //string uri = setUri.Uri.ToString();


    //Console.WriteLine(uri);
    //uri = new WebHtml().GetDosarUri(uri);
    //Console.WriteLine(uri);
    //string html = new WebHtml().GetDosarData(uri);


    //Console.WriteLine(html);

    //Console.WriteLine(new WebHtml().GetDosarData(uri));
    //Console.ReadLine();

}