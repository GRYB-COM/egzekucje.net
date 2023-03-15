using BOS.NET;
using System.Collections.Generic;

namespace Egzekucje.NET.Adapters
{
    public class LocalBosService : Egzekucje.NET.Bos.BosService
    {
        readonly BosApplicationService bosApplicationService = new BosApplicationService();
        public Adresat PobierzDaneAdresata(long idOsoby, long idAdresu)
        {
            Osoba osoba = bosApplicationService.PobierzDaneOsoby(idOsoby, idAdresu);
            Adresat adresat = new Adresat(
                osoba.IdOsoby,
                osoba.IdAdresu,
                osoba.Nip,
                osoba.Pesel,
                osoba.Imie,
                osoba.Nazwisko,
                osoba.Nazwa,
                osoba.Miejscowosc,
                osoba.TypUlicy,
                osoba.Ulica,
                osoba.NrDomu,
                osoba.NrLokalu,
                osoba.KodPocztowy,
                osoba.Poczta
                );

            return adresat;
        }
        public List<Adres> PobierzAdresyAdresata(long idOsoby)
        {
            List<Adres> adresy = bosApplicationService.PobierzAdresyOsoby(idOsoby);
            return adresy;
        }
    }
}