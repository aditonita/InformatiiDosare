// See https://aka.ms/new-console-template for more information

using InformatiiDosare;

//SetUri setUri = new SetUri("https://portal.just.ro", "16944/215/2020");
foreach(string line in IODosar.GetNrDosare("input"))
{
    
    SetUri setUri = new SetUri("https://portal.just.ro", line);
    string uri = setUri.Uri.ToString();
    new WebHtml().GetLinkDosar(uri);
    //uri = new WebHtml().GetDosarUri(uri);
    //IODosar.SaveDosarData(new WebHtml().GetDosarData(uri));
}
//string uri = setUri.Uri.ToString();
 

//Console.WriteLine(uri);
//uri = new WebHtml().GetDosarUri(uri);
//Console.WriteLine(uri);
//string html = new WebHtml().GetDosarData(uri);


//Console.WriteLine(html);

//Console.WriteLine(new WebHtml().GetDosarData(uri));
//Console.ReadLine();

