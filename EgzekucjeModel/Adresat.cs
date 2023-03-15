using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egzekucje.NET
{
    // SMELL czy to na pewno klasa w tym BC?
    [ComplexType]
    public class Adresat
    {
        public Adresat() { }
        public Adresat(long idOsoby, long idAdresu)
        {
            this.IdOsoby = idOsoby;
            this.IdAdresu = idAdresu;
        }

        public Adresat(long idOsoby, long idAdresu, string nip, string pesel, string imie, string nazwisko, string nazwa, string miejscowosc, string typUlicy, string ulica, string nrDomu, string nrLokalu, string kodPocztowy, string poczta)
            : this(idOsoby, idAdresu)
        {
            Nip = nip;
            Pesel = pesel;
            Imie = imie;
            Nazwisko = nazwisko;
            Nazwa = nazwa;
            Miejscowosc = miejscowosc;
            TypUlicy = typUlicy;
            Ulica = ulica;
            NrDomu = nrDomu;
            NrLokalu = nrLokalu;
            KodPocztowy = kodPocztowy;
            Poczta = poczta;
        }
        [Key]
        [Column("ADRESAT_ID_OSOBY")]
        [JsonProperty("ADRESAT_ID_OSOBY")]
        public long IdOsoby { get; set; }
        // [Key]
        [Column("ADRESAT_ID_ADRESU")]
        [JsonProperty("ADRESAT_ID_ADRESU")]
        public long IdAdresu { get; set; }
        [Column("ADRESAT_NIP")]
        [JsonProperty("ADRESAT_NIP")]
        public string Nip { get; private set; }
        [Column("ADRESAT_PESEL")]
        [JsonProperty("ADRESAT_PESEL")]
        public string Pesel { get; private set; }
        [Column("ADRESAT_IMIE")]
        [JsonProperty("ADRESAT_IMIE")]
        public string Imie { get; private set; }
        [Column("ADRESAT_NAZWISKO")]
        [JsonProperty("ADRESAT_NAZWISKO")]
        public string Nazwisko { get; private set; }
        [Column("ADRESAT_NAZWA")]
        [JsonProperty("ADRESAT_NAZWA")]
        public string Nazwa { get; private set; }
        [Column("ADRESAT_MIEJSCOWOSC")]
        [JsonProperty("ADRESAT_MIEJSCOWOSC")]
        public string Miejscowosc { get; private set; }
        [Column("ADRESAT_TYP_ULICY")]
        [JsonProperty("ADRESAT_TYP_ULICY")]
        public string TypUlicy { get; private set; }
        [Column("ADRESAT_ULICA")]
        [JsonProperty("ADRESAT_ULICA")]
        public string Ulica { get; private set; }
        [Column("ADRESAT_NR_DOMU")]
        [JsonProperty("ADRESAT_NR_DOMU")]
        public string NrDomu { get; private set; }
        [Column("ADRESAT_NR_LOKALU")]
        [JsonProperty("ADRESAT_NR_LOKALU")]
        public string NrLokalu { get; private set; }
        [Column("ADRESAT_KOD_POCZTOWY")]
        [JsonProperty("ADRESAT_KOD_POCZTOWY")]
        public string KodPocztowy { get; private set; }
        [Column("ADRESAT_POCZTA")]
        [JsonProperty("ADRESAT_POCZTA")]
        public string Poczta { get; private set; }
    }

}