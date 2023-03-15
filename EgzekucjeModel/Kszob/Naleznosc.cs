using System;

namespace Egzekucje.NET.Kszob
{
    public class Naleznosc
    {
        public long IdNaleznosci { get; set; }
        public long IdZobowiazania { get; set; }
        public String NazwaZobowiazania { get; set; }
        public String SkrotwaNazwaZobowiazania { get; set; }
        public long IdSystemuWymiarowego { get; set; }
        public String NazwaSystemuWymiarowego { get; set; }
        public String SkrotowaNazwaSystemuWymiarowego { get; set; }
        public long IdKontaWymiarowego { get; set; }
        public long IdOsoby { get; set; }
        public int RokObrotowy { get; set; }

        public static Egzekucje.NET.Kszob.Naleznosc StworzZ(global::Kszob.NET.Naleznosc nal)
        {
            return new Egzekucje.NET.Kszob.Naleznosc
            {
                IdNaleznosci = nal.IdNaleznosci,
                IdZobowiazania = nal.IdZobowiazania,
                NazwaZobowiazania = nal.NazwaZobowiazania,
                SkrotwaNazwaZobowiazania = nal.SkrotowaNazwaZobowiazania,
                IdSystemuWymiarowego = nal.IdSystemuWymiarowego,
                NazwaSystemuWymiarowego = nal.NazwaSystemuWymiarowego,
                SkrotowaNazwaSystemuWymiarowego = nal.SkrotowaNazwaSystemuWymiarowego,
                IdKontaWymiarowego = nal.IdKontaWymiarowego,
                IdOsoby = nal.IdOsoby,
                RokObrotowy = nal.RokObrotowy,
                Rata = nal.Rata,
                IdRaty = nal.IdRaty,
                TerminPlatnosci = nal.TerminPlatnosci,
                StawkaVAT = nal.StawkaVAT,
                KwotaNaleznosci = nal.KwotaNaleznosci,
                KwotaOdsetek = nal.KwotaOdsetek
            };
        }

        public decimal Rata { get; set; }
        public long IdRaty { get; set; }

        public Zaleglosc PrzeksztalcWZaleglosc()
        {
            return new Zaleglosc()
            {
                IdNaleznosci = this.IdNaleznosci,
                IdOsoby = this.IdOsoby,
                IdKontaWymiarowego = this.IdKontaWymiarowego,
                IdSystemuWymiarowego = this.IdSystemuWymiarowego,
                IdZobowiazania = this.IdZobowiazania,
                IdRaty = this.IdRaty,
                Rata = this.Rata,
                KwotaZaleglosci = this.KwotaNaleznosci,
                KwotaOdsetek = this.KwotaOdsetek,
                StawkaVAT = this.StawkaVAT,
                RokObrotowy = this.RokObrotowy,
                TerminPlatnosci = this.TerminPlatnosci
            };
        }

        public DateTime TerminPlatnosci { get; set; }
        public string StawkaVAT { get; set; }
        public decimal KwotaNaleznosci { get; set; }
        public decimal KwotaOdsetek { get; set; }
    }
}
