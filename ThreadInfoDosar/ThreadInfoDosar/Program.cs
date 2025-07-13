// See https://aka.ms/new-console-template for more information

using InformatiiDosare;
using System;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

string index = String.Empty;
if (args.Length > 0)
{
    index = args[0];
}
if (args.Length > 1)
{
    new Dosare().Start(index, args.Skip(1).Take(args.Length - 1).ToArray());
}
