using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class CustomArguments
    {
        #region public
        public static void Start(string[] args)
        {
            if (IsHelp(args))
            {
                WriteHelpMessage(args);
                return;
            }
            if (IsNrDosar(args))
            {
                WritePortalUriAttributes(args);
                return;
            }
            if (IsIdInstanta(args))
            {
                WriteDosarInstantaUriAttributes(args);
                return;
            }
            if (IsNumeInstanta(args))
            {
                WriteDosarNumeInstantaUriAttributes(args);
                return;
            }
            throw new InvalidArgsException("[ERROR] - pentru ajutor rulati: InformatiiDosare.exe --help");
        }
        #endregion
        #region private
        private static bool IsNrDosar(string[] args)
        {
            if (args.Length != 2)
            {
                return false;
            }
            foreach (string arg in args)
            {
                if (arg == "--nrDosar")
                {
                    return true;
                }
            }
            return false;
        }
        private static bool IsIdInstanta(string[] args)
        {
            bool hasInstanta = false;
            bool hasDosar = false;
            if (args.Length != 4)
            {
                return false;
            }
            foreach (string arg in args)
            {
                if (arg == "--idInstanta")
                {
                    hasInstanta = true;
                }
                if (arg == "--idDosar")
                {
                    hasDosar = true;
                }
            }
            return hasInstanta && hasDosar;
        }
        private static bool IsNumeInstanta(string[] args)
        {
            bool hasInstanta = false;
            bool hasDosar = false;
            bool hasNumeInstanta = false;
            if (args.Length != 6)
            {
                return false;
            }
            foreach (string arg in args)
            {
                if (arg == "--idInstanta")
                {
                    hasInstanta = true;
                }
                if (arg == "--idDosar")
                {
                    hasDosar = true;
                }
                if (arg == "--instanta")
                {
                    hasNumeInstanta = true;
                }
            }
            return hasInstanta && hasDosar && hasNumeInstanta;
        }
        private static bool IsHelp(string[] args)
        {
            if (args.Length != 1)
            {
                return false;
            }
            foreach (var arg in args)
            {
                if (arg == "--help")
                {
                    return true;
                }
            }
            return false;
        }
        private static void WriteHelpMessage(string[] args)
        {
            string message =
                "InformatiiDosare.exe" + Environment.NewLine +
                "InformatiiDosare.exe --help" + Environment.NewLine +
                "InformatiiDosare.exe --rnDosar nnnn/tttt/yyyy" + Environment.NewLine +
                "InformatiiDosare.exe --idInstanta xxxxx --idDosar yyyyyyyyyyyyyyy" + Environment.NewLine +
                "InformatiiDosare.exe --idInstanta xxxxx --idDosar yyyyyyyyyyyyyyy --instanta true/false" + Environment.NewLine +
                "1.  InformatiiDosare.exe" + Environment.NewLine +
                "    Genereaza fisierele:" + Environment.NewLine +
                "    * URI_dosare.csv - contine URL catre dosar instanta. folositi orice browser pentru a vedea detalii;" + Environment.NewLine +
                "    * InformatiiGenerale.csv, Parti.csv, Sedinte.csv, CaiAtac.csv" +
                " cu detalii despre dosarele din fisierul input. Pentri informatii complete folositi URL." + Environment.NewLine +
                "    Fisierul input are pe fiecare linie numarul dosarului. ex:" + Environment.NewLine +
                "    41738/94/2021" + Environment.NewLine +
                "    423/3/2021" + Environment.NewLine +
                "    5675/299/2023" + Environment.NewLine +
                "2.  Argumente:" + Environment.NewLine +
                "    --help: editeaza acest help" + Environment.NewLine +
                "    --nrDosar 5675/299/2023: Afiseaza nume tag <Informatii dosar> ca bytes din pagina https://portal.just.ro/SitePages/cautare.aspx?k=5675/299/2023" + Environment.NewLine +
                "    --idInstanta 299 --idDosar 29900000000992959: Afiseaza numele tag-urilor <Informatii generale>, <Sedinte>, <Cai atac>, <Parti>, <citare prin publicitate> ca bytes din pagina https://portal.just.ro/299/SitePages/Dosar.aspx?id_dosar=29900000000992959&id_inst=299" + Environment.NewLine +
                "    --idInstanta 299 --idDosar 29900000000992959 --instanta true: Afiseaza nume tag <Pagina_principala> ca bytes din pagina " +
                "https://portal.just.ro/299/SitePages/Dosar.aspx?id_dosar=29900000000992959&id_inst=299" + Environment.NewLine;
            if (IsHelp(args))
            {
                Console.WriteLine(message);
            }
        }
        private static string GetArgsValue(string[] args, string argName)
        {

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == argName)
                {
                    return args[i + 1];
                }
                if (args[i] == argName)
                {
                    return args[i + 1];
                }
                if (args[i] == argName)
                {
                    return args[i + 1];
                }
                if (args[i] == argName)
                {
                    return args[i + 1];
                }
            }
            return string.Empty;
        }
        private static void WritePortalUriAttributes(string[] args)
        {
            Console.WriteLine(Utils.GetAttributName(GetArgsValue(args, "--nrDosar")));
        }
        private static void WriteDosarInstantaUriAttributes(string[] args)
        {
            Console.WriteLine(Utils.GetAttributName(GetArgsValue(args, "--idInstanta"), GetArgsValue(args, "--idDosar")));
        }
        private static void WriteDosarNumeInstantaUriAttributes(string[] args)
        {
            Console.WriteLine(Utils.GetAttributName(GetArgsValue(args, "--idInstanta"), GetArgsValue(args, "--idDosar"), GetArgsValue(args, "--instanta")));
        }
        #endregion
    }
    [Serializable]
    class InvalidArgsException : Exception
    {
        public InvalidArgsException() : base() { }
        public InvalidArgsException(string message) : base(message) { }
        public InvalidArgsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
