// See https://aka.ms/new-console-template for more information

using InformatiiDosare;
using System;

/*arguments
 * --NrDosar
 * --IdInstanta
 * --IdDosar
 * --help
*/

char delim = ',';
string outputFile = "URI_dosare.csv";
string inputFile = "input.csv";
string informatiiGenerale = "Informatii_generale.csv";
string parti = "Parti.csv";
string sedinte = "Sedinte.csv";
string caiAtac = "Cai_atac.csv";
Dictionary<string, string> outFiles = new Dictionary<string, string>()
{
    { "informatiiGenerale" , informatiiGenerale },
    { "parti", parti },
    { "sedinte", sedinte },
    { "caiAtac", caiAtac }
};


if (args.Length == 0)
{
    Console.WriteLine("Press y to contine [Y/N] or run InformatiiDosare.exe --help for help page");
    string? KeyPress = Console.ReadLine()??"";
    if (KeyPress.ToLower() != "y")
    {
        Environment.Exit(1);
    }
    IODosar.RemoveFiles(outputFile, informatiiGenerale, parti, sedinte, caiAtac);
    IODosar.GetNrDosare(inputFile, outputFile, delim);
    IODosar.SaveInformatiiGenerale(outputFile, outFiles, delim);
    try
    {
        foreach (string dosarUri in IODosar.GetDosarURIs(outputFile, delim))
        {
            Console.WriteLine(dosarUri);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message.ToString());
    }
}
else
{
    new CustomArguments(args).WriteAtributeAsBytes();
    Console.ReadKey();
}