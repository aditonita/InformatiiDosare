// See https://aka.ms/new-console-template for more information

using InformatiiDosare;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

if (args.Length == 0)
{
    new Dosare().Start();
}
else
{
    CustomArguments.Start(args);
}
