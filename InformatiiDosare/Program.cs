// See https://aka.ms/new-console-template for more information

using InformatiiDosare;

char delim = ',';

//SetUri setUri = new SetUri("https://portal.just.ro", "16944/215/2020");
foreach(string line in IODosar.GetNrDosare("input"))
{
    Console.WriteLine(line);
    SetUri setUri = new SetUri("https://portal.just.ro", line);
    string uri = setUri.Uri.ToString();
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


//Console.WriteLine(uri);
//uri = new WebHtml().GetDosarUri(uri);
//Console.WriteLine(uri);
//string html = new WebHtml().GetDosarData(uri);


//Console.WriteLine(html);

//Console.WriteLine(new WebHtml().GetDosarData(uri));
//Console.ReadLine();

