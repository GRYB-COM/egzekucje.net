using InfoSystem.Templates;
using IniParser;
using IniParser.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egzekucje.NET
{
    [Table("EGZ_UPOMNIENIA")]
    public class Upomnienie
    {
        [Key]
        [Column("ID_UPOMNIENIA")]
        [JsonProperty("ID_UPOMNIENIA")]
        public long IdUpomnienia { get; set; }
        [JsonProperty("Adresat")]
        public Adresat Adresat { get; private set; }

        [JsonProperty("Zaleglosci")]
        public List<Zaleglosc> Zaleglosci { get; private set; } = new List<Zaleglosc>();

        [Column("KOSZT_UPOMNIENIA")]
        [JsonProperty("KOSZT_UPOMNIENIA")]
        public decimal KosztUpomnienia { get; private set; }
        [Column("DATA_UPOMNIENIA")]
        [JsonProperty("DATA_UPOMNIENIA")]
        public DateTime DataUpomnienia { get; private set; }

        public Upomnienie() { }
        public Upomnienie(string jsonString) 
        {
             Newtonsoft.Json.JsonConvert.DeserializeObject<Upomnienie>(jsonString);
        }

        public Upomnienie(Adresat adresat, List<Zaleglosc> zaleglosci, DateTime dataUpomnienia)
        {
            Adresat = adresat;
            Zaleglosci = zaleglosci;
            KosztUpomnienia = WczytajKosztyUpomnienia();
            DataUpomnienia = dataUpomnienia;
        }

        public Upomnienie(long idUpomnienia, long idOsoby, long idAdresu, decimal kosztUpomnienia, DateTime dataUpomnienia)
        {
            this.IdUpomnienia = idUpomnienia;
            this.Adresat = new Adresat(idOsoby, idAdresu);
            this.KosztUpomnienia= kosztUpomnienia;
            this.DataUpomnienia = dataUpomnienia;
        }

        public string PobierzRtf()
        {
            var szablonZaleglosci = new RtfTemplate<Zaleglosc>()
                .Macro("@uLP", (z, p) => "1", "Lp")
                .Macro("@uRataRokObr", (z, p) => z.TerminPlatnosci.Year.ToString(), "Rok obrotowy")
                .Macro("@uKwotaPoz", (z, p) => z.KwotaZaleglosci.ToString("n2"), "Kwota zaległości")
                .Macro("@uDataOds", (z, p) => z.TerminPlatnosci.ToString("dd-MM-yyyy"), "Termin płatności")
                .Macro("@uOdsPoz", (z, p) => z.KwotaOdsetek.ToString("n2"), "Kwota odseteK")
                .Macro("@uFullNazZob", (z, p) => "Nazwa zob.", "Nazwa zobowiązania")
                .Macro("@uPdstPrw", (z, p) => "Podst. prawna", "Podstwa prawna")
                .Macro("@uStawkaVAT", (z, p) => "23%", "Stawka VAT");

            //var szablon = new RtfTemplate<Upomnienie>("D:/VS2019/egzekucje.net/EgzekucjeREST3/bin/Debug/net472/Zasoby/upomnienr.rtf")
            var szablon = new RtfTemplate<Upomnienie>("Zasoby/upomnienr.rtf")
                .Context(this)
                .Macro("@uNumer", (c, p) => c.IdUpomnienia.ToString(), "Numer upomnienia")
                .Macro("@uRok", (c, p) => c.DataUpomnienia.Year.ToString(), "Data wystawienia")
                .Macro("@uData", (c, p) => c.DataUpomnienia.ToShortDateString(), "Data wystawienia")
                .Macro("@uNazwa", (c, p) => c.Adresat.Nazwa, "Nazwa adresata")
                .Macro("@uUlica", (c, p) => c.Adresat.Ulica, "Ulica")
                .Macro("@uMiasto", (c, p) => c.Adresat.Miejscowosc, "Miasto")
                .Macro("@uPesReg", (c, p) => c.Adresat.Pesel, "Pesel/Regon")
                .Macro("@uPozycjeWgWzorca", (c, p) => RtfTemplate<Zaleglosc>.GenerujTabele(c.Zaleglosci, szablonZaleglosci, p), "Pozycje upomnienia")
                .Macro("@uStawkaOds", (c, p) => c.KosztUpomnienia.ToString("n2"), "");

            return szablon.Parse();
        }

        // TODO formalnie to powinno być poprzez port
        private static decimal WczytajKosztyUpomnienia()
        {
            var parser = new FileIniDataParser();

            IniData data = parser.ReadFile("Zasoby/Kszob.ini");

            string kosztString = data["UPOMNIENIA"]["Koszty"];

            return decimal.Parse(kosztString);
        }
    }

    class ZaleglosciUpomnienia
    {
        [Key]
        [Column("ID_ZALEGLOSCI_UPOMNIENIA")]
        public long IdZaleglosciUpomnienia { get; private set; }

        [Column("ID_UPOMNIENIA")]
        [ForeignKey("IdUpomnienia")]
        public Upomnienie upomnienie { get; private set; }

        [Column("ID_ZALEGLOSCI")]
        public long IdZaleglosci { get; private set; }

        [Column("KWOTA_ZALEGLOSCI")]
        public decimal KwotaZaleglosci { get; private set; }
    }
}
