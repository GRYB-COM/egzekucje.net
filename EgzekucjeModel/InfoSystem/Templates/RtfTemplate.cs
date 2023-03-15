using Egzekucje.NET;
using ESCommon.Rtf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace InfoSystem.Templates
{
    public class RtfTemplate<T>
    {
        private string path;
        private T context;
        private Dictionary<string, MacroDef<T>> macros = new Dictionary<string, MacroDef<T>>();

        public RtfTemplate() {}

        public RtfTemplate(string path)
        {
            this.path = path;
        }

        public RtfTemplate<T> Context(T context)
        {
            this.context = context;
            return this;
        }

        public RtfTemplate<T> Macro(string name, Func<T, string, string> macro, string description)
        {
            this.macros[name] = new MacroDef<T>(name, macro, description);

            return this;
        }

        public string Parse(T context, string text)
        {
            return ReplaceMacros(context, text);
        }

        public string Parse()
        {
            if (path == null) throw new RtfTemplateException("Use RtfTemplate(string path) constructor before or use Parse(context, text) instead");

            if (context == null) throw new RtfTemplateException("Use Context(context) methos before");

            if (!File.Exists(path))
            {
                throw new RtfTemplateException(string.Format("Path for rtf template '{0}' doesn't exist", path));
            }

            string text = File.ReadAllText(path);

            string replaced = ReplaceMacros(this.context, text);

            return replaced;
        }

        private string ReplaceMacros(T context, string text)
        {
            text = $" {text}";
            return Regex.Replace(text, @".(@.+?)\((.*?)\)", m =>
            {
                var macroName = DiscardWrongChars(m.Groups[1].Value);
                var param = DiscardWrongChars(m.Groups[2].Value);

                if (this.macros.ContainsKey(macroName) == false)
                {
                    return " <<Nie rozpoznano makra: " + macroName + ">>";
                }
                return this.macros[macroName].Macro.Invoke(context, param);
            }).TrimStart();

        }

        public static string GenerujTabele(List<T> lista, RtfTemplate<T> szablonDlaListy, string parametry)
        {
            string[] podzieloneParametry = parametry.Split(',');
            if (podzieloneParametry.Length == 0)
            {
                throw new EgzekucjeException("Definicja tabeli powinna zawierać nazwę pliku z konfiguracją.");
            }

            var opisTabeli = OpisTabeli.WczytajOpisTabeli(podzieloneParametry[0]);

            RtfDocument rtf = new RtfDocument();
            // + 1 bo nagłówek
            RtfTable tabela = new RtfTable(RtfTableAlign.Center, opisTabeli.KolumnyNaglowka.Count, lista.Count + 1);
            RtfParagraphFormatting centered9 = new RtfParagraphFormatting(9, RtfTextAlign.Center);
            centered9.FontIndex = opisTabeli.OpisNaglowka.StylCzcionki;
            tabela.DefaultCellStyle = new RtfTableCellStyle(RtfBorderSetting.All, centered9);

            int indeks = 0;
            foreach (string klucz in opisTabeli.KolumnyNaglowka.Keys)
            {
                OpisElementu opis = opisTabeli.KolumnyNaglowka[klucz];

                tabela[indeks, 0].Definition.Style = StworzStyl(opisTabeli.OpisNaglowka, opis);
                tabela.Columns[indeks].Width = (int)(opis.SzerokoscKolumny * 0.8);
                tabela[indeks, 0].AppendText($"{opis.Tresc.Replace("<br>", "")}");

                indeks++;
            }

            for (int nrWiersza = 1; nrWiersza <= lista.Count; nrWiersza++)
            {
                T elementListy = lista[nrWiersza - 1];
                szablonDlaListy.Macro("@uLP", (z, p) => nrWiersza.ToString(), "Lp");

                indeks = 0;
                foreach (string klucz in opisTabeli.KolumnyWiersza.Keys)
                {
                    OpisElementu opis = opisTabeli.KolumnyWiersza[klucz];
                    tabela[indeks, nrWiersza].Definition.Style = StworzStyl(opisTabeli.OpisWiersza, opis);

                    string tresc = szablonDlaListy.Parse(elementListy, opis.Tresc);
                    tabela[indeks++, nrWiersza].AppendText($"{tresc}");
                }
            }

            rtf.Contents.Add(new RtfFormattedParagraph());
            rtf.Contents.Add(tabela);
            rtf.Contents.Add(new RtfFormattedParagraph());

            StringWriter stringWriter = new StringWriter();
            new RtfWriter().Write(stringWriter, rtf);
            stringWriter.Close();

            return PrzytnijRtfDoTabeli(stringWriter.ToString());
        }

        private static string PrzytnijRtfDoTabeli(string rtfString)
        {
            // usun prefiks rtf
            const int dlugoscPrefiksuRtf = 52;
            string rtfTabeli = rtfString.Substring(dlugoscPrefiksuRtf, rtfString.Length - dlugoscPrefiksuRtf);
            // usun ostatnia klamre
            return rtfTabeli.Substring(0, rtfTabeli.Length - 1);
        }

        private static RtfTableCellStyle StworzStyl(OpisCzesci opisCzesci, OpisElementu opisElementu)
        {
            var styl = new RtfTableCellStyle();
            if (opisElementu.WyrownaniePoziome.HasValue)
            {
                var paragraph = new RtfParagraphFormatting(9);
                paragraph.BackgroundColorIndex = opisCzesci.KolorTla;
                paragraph.Align = opisElementu.WyrownaniePoziome.Value.GetEnumValue<RtfTextAlign>();
                styl.DefaultParagraphFormatting = paragraph;
            }

            return styl;
        }

        private string DiscardWrongChars(string text)
        {
            return Regex.Replace(text, @".(\\f.*?\s)", ""); 
        }
    }
}
