using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformatiiDosare
{
    internal class CustomArguments
    {
        Dictionary<string, string> _arguments;
        string[] _args = Array.Empty<string>();
        #region constructors
        public CustomArguments(string[] args)
        {
            _args = args;  
            _arguments = new Dictionary<string, string>
            {
                { "nrDosar", "--NrDosar" },
                { "idInstanta", "--IdInstanta" },
                { "idDosar", "--IdDosar" },
                { "help", "--help" }
            };
        }
        #endregion
        #region public
        public void WriteAtributeAsBytes()
        {
            if (IsHelp())
            {
                WriteHelpMessage();
                return;
            }
            if(IsNrDosar())
            {
                WritePortalUriAttributes();
                return;
            }
            if(IsIdInstanta())
            {
                WriteDosarInstantaUriAttributes();
                return;
            }
            throw new InvalidArgsException("[ERROR] - pentru ajutor rulati: InformatiiDosare.exe --help");
        }
        #endregion
        #region private
        private bool IsNrDosar()
        {
            if(_args.Length != 2)
            {
                return false;
            }
            foreach(string arg in _args)
            {
                if(arg == _arguments["nrDosar"])
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsIdInstanta()
        {
            bool hasInstanta = false;
            bool hasDosar = false;
            if (_args.Length != 4)
            {
                return false;
            }
            foreach(string arg in _args)
            {
                if(arg == _arguments["idInstanta"])
                {
                    hasInstanta = true;
                }
                if(arg == _arguments["idDosar"])
                {
                    hasDosar = true;
                }
            }
            return hasInstanta && hasDosar;
        }
        private bool IsHelp()
        {
            if(_args.Length != 1)
            {
                return false;
            }
            foreach (var arg in _args)
            {
                if (arg == _arguments["help"])
                {
                    return true;
                }
            }
            return false;
        }
        private void WriteHelpMessage()
        {
            string message = 
                "InformatiiDosare.exe" + Environment.NewLine +
                "InformatiiDosare.exe --help" + Environment.NewLine +
                "InformatiiDosare.exe --NrDosar nnnn/tttt/yyyy" + Environment.NewLine +
                "InformatiiDosare.exe --IdInstanta xxxxx --IdDosar yyyyyyyyyyyyyyy" + Environment.NewLine +
                "1.  InformatiiDosare.exe" + Environment.NewLine +
                "    Genereaza fisierele:" + Environment.NewLine +
                "    * URI_dosare.csv - contine URL catre dosar instanta. folositi orice browser pentru a vedea detalii;" + Environment.NewLine +
                "    * InformatiiGenerale.csv, Parti.csv, Sedinte.csv, CaiAtac.csv" +
                " cu detalii despre dosarele din fisierul input. Pentri informatii complete folositi URL." + Environment.NewLine +
                "    Fisierul input are pe fiecare linie numarul dosarului. ex:"+ Environment.NewLine + 
                "    41738/94/2021" + Environment.NewLine + 
                "    423/3/2021" + Environment.NewLine + 
                "    5675/299/2023" + Environment.NewLine +
                "2.  Argumente:" + Environment.NewLine +
                "    --help: editeaza acest help" + Environment.NewLine +
                "    --NrDosar 5675/299/2023: Afiseaza numele tag-urilor ca bytes din pagina https://portal.just.ro/SitePages/cautare.aspx?k=5675/299/2023" + Environment.NewLine +
                "    --IdInstanta 299 --IdDosar 29900000000992959: Afiseaza numele tag-urilor ca bytes din pagina https://portal.just.ro/299/SitePages/Dosar.aspx?id_dosar=29900000000992959&id_inst=299" + Environment.NewLine;
            if(IsHelp())
            {
                Console.WriteLine(message);
            }
        }
        private Dictionary<string, string> GetArgsValue()
        {
            Dictionary<string,string> argsValue = new Dictionary<string,string>();
            for(int i = 0; i < _args.Length; i++)
            {
                if (_args[i] == _arguments["nrDosar"])
                {
                    argsValue.Add("nrDosar", _args[i + 1]);
                }
                if (_args[i] == _arguments["idInstanta"])
                {
                    argsValue.Add("idInstanta", _args[i + 1]);
                }
                if (_args[i] == _arguments["idDosar"])
                {
                    argsValue.Add("idDosar", _args[i + 1]);
                }
            }
            return argsValue;
        }
        private void WritePortalUriAttributes()
        {
            Console.WriteLine(Tag.GetAttributName(GetArgsValue()["nrDosar"]));
        }
        private void WriteDosarInstantaUriAttributes()
        {
            Console.WriteLine(Tag.GetAttributName(GetArgsValue()["idInstanta"], GetArgsValue()["idDosar"]));
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
