using IniParser;
using IniParser.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoSystem.Templates
{
    public class OpisTabeli
    {
        public int IloscStopek = 0;
        public OpisCzesci OpisNaglowka;
        public Dictionary<string, OpisElementu> KolumnyNaglowka = new Dictionary<string, OpisElementu>();

        public OpisCzesci OpisWiersza { get; set; }
        public Dictionary<string, OpisElementu> KolumnyWiersza = new Dictionary<string, OpisElementu>();

        public OpisCzesci OpisStopki;
        public Dictionary<string, OpisElementu> KolumnyStopki = new Dictionary<string, OpisElementu>();

        public static OpisTabeli WczytajOpisTabeli(string filename)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(filename, Encoding.Default);

            OpisTabeli opisTabeli = new OpisTabeli();

            var sections = data.Sections;

            UpewnijSieZeZawiera(sections, "TABLE");
            UpewnijSieZeZawiera(sections, "HEADER");
            UpewnijSieZeZawiera(sections, "ROW");
            UpewnijSieZeZawiera(sections, "FOOTER");

            UstawJesliZdefiniowane(sections["TABLE"], "Footers", ref opisTabeli.IloscStopek);

            var opisNaglowka = new OpisCzesci();
            UstawJesliZdefiniowane(sections["HEADER"], "styl czcionki", ref opisNaglowka.StylCzcionki);
            UstawJesliZdefiniowane(sections["HEADER"], "kolor tła", ref opisNaglowka.KolorTla);
            opisTabeli.OpisNaglowka = opisNaglowka;

            var opisWiersza = new OpisCzesci();
            UstawJesliZdefiniowane(sections["ROW"], "styl czcionki", ref opisWiersza.StylCzcionki);
            UstawJesliZdefiniowane(sections["ROW"], "kolor tła", ref opisWiersza.KolorTla);
            opisTabeli.OpisWiersza = opisWiersza;

            var opisStopki = new OpisCzesci();
            UstawJesliZdefiniowane(sections["FOOTER"], "styl czcionki", ref opisStopki.StylCzcionki);
            UstawJesliZdefiniowane(sections["FOOTER"], "kolor tła", ref opisStopki.KolorTla);
            opisTabeli.OpisStopki = opisStopki;

            opisTabeli.KolumnyNaglowka = WczytajKolumnyNaglowka(sections);
            opisTabeli.KolumnyWiersza = WczytajKolumnyWiersza(sections);
            opisTabeli.KolumnyStopki = WczytajKolumnyStopki(sections);
            return opisTabeli;
        }

        private static Dictionary<string, OpisElementu> WczytajKolumnyStopki(SectionDataCollection sections)
        {
            return WczytajKolumny(sections, "FOOTER_");
        }

        private static Dictionary<string, OpisElementu> WczytajKolumnyWiersza(SectionDataCollection sections)
        {
            return WczytajKolumny(sections, "ROW_");
        }

        private static Dictionary<string, OpisElementu> WczytajKolumnyNaglowka(SectionDataCollection sections)
        {
            return WczytajKolumny(sections, "HEADER_");
        }

        private static Dictionary<string, OpisElementu> WczytajKolumny(SectionDataCollection sections, string prefix)
        {
            Dictionary<string, OpisElementu> kolumny
                = sections.Select(s => s.SectionName)
                    .Where(name => name.StartsWith(prefix) && name.IndexOf('_') < name.Length - 1)
                    .Select(name =>
                    {
                        var opis = new OpisElementu();
                        opis.Nazwa = name;
                        UstawJesliZdefiniowane(sections[name], "szerokość kolumny", ref opis.SzerokoscKolumny);
                        UstawJesliZdefiniowane(sections[name], "treść", ref opis.Tresc);
                        UstawJesliZdefiniowane(sections[name], "wyrównanie poziome", ref opis.WyrownaniePoziome);
                        return opis;
                    })
                    .ToDictionary(o => o.Nazwa.Substring(o.Nazwa.IndexOf('_') + 1), o => o);

            return kolumny;
        }

        private static void UpewnijSieZeZawiera(SectionDataCollection sections, string name)
        {
            if (sections.ContainsSection(name) == false)
            {
                throw new OpisTabeliException(string.Format("Plik konfiguracyjny tabeli powinien zawierać sekcję [{0}]", name));
            }
        }

        public static void UstawJesliZdefiniowane(KeyDataCollection czesc, string klucz, ref string wlasciwosc)
        {
            if (czesc.ContainsKey(klucz))
            {
                wlasciwosc = czesc[klucz];
            }
        }

        public static void UstawJesliZdefiniowane(KeyDataCollection czesc, string klucz, ref int wlasciwosc)
        {
            if (czesc.ContainsKey(klucz))
            {
                int wartosc;
                var ok = int.TryParse(czesc[klucz], out wartosc);

                if (ok)
                {
                    wlasciwosc = wartosc;
                    return;
                }
                else
                {
                    throw new OpisTabeliException($"Wartość dla klucza {klucz} powinna być liczbą całkowitą, a jest '{czesc[klucz]}'");
                }
            }
        }

        public static void UstawJesliZdefiniowane(KeyDataCollection czesc, string klucz, ref int? wlasciwosc)
        {
            if (czesc.ContainsKey(klucz))
            {
                int wartosc;
                var ok = int.TryParse(czesc[klucz], out wartosc);

                if (ok)
                {
                    wlasciwosc = wartosc;
                    return;
                }
                else
                {
                    throw new OpisTabeliException($"Wartość dla klucza {klucz} powinna być liczbą całkowitą, a jest '{czesc[klucz]}'");
                }
            }
        }
    }

    public class OpisCzesci
    {
        public int StylCzcionki = 0;
        public int KolorTla = 12;
    }

    public class OpisElementu
    {
        public string Nazwa = string.Empty;
        public int SzerokoscKolumny = 100;
        public string Tresc = string.Empty;
        public int? WyrownaniePoziome = null;
    }
}
