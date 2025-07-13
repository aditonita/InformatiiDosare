// See https://aka.ms/new-console-template for more information

using AgregareInformatiiDosare;
using InformatiiDosare;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

IOControler.RemoveFiles(Utils.APP_PATH);

List<string[]> dosareinIterarie = new Dosar().GetDosareByIteration(Utils.DOSARE_IN_ITERATIE);

new Dosar().RunInformatiiDosare(Utils.APP_PATH, dosareinIterarie);
