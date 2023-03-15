using Egzekucje.NET.Kszob;
using Kszob.NET;
using System.Collections.Generic;
using System.Linq;

namespace Egzekucje.NET.Adapters
{
    public class LocalKszobService : Egzekucje.NET.Kszob.KszobService
    {
        private KszobApplicationService kszobApplicationService = new KszobApplicationService();

        public List<Egzekucje.NET.Kszob.Naleznosc> PobierzPrzeterminowaneNaleznosciNaBiezacyDzien()
        {
            var naleznosci = kszobApplicationService.PobierzPrzeterminowaneNaleznosciNaBiezacyDzien();
            return naleznosci.Select(nal => Egzekucje.NET.Kszob.Naleznosc.StworzZ(nal)).ToList();
        }

        public List<Egzekucje.NET.Kszob.Naleznosc> PobierzPrzeterminowaneNaleznosciOsoby(long idOsoby)
        {
            var naleznosci = kszobApplicationService.PobierzPrzeterminowaneNaleznosciOsoby(idOsoby);

            return naleznosci.Select(nal => Egzekucje.NET.Kszob.Naleznosc.StworzZ(nal)).ToList();
        }

        // SMELL prawdopodobnie to nie powinno isc przez egzekucje.net
        public List<Egzekucje.NET.Kszob.IdKontaWAdresu>  PobierzListeKontDlaAdresu(long idAdresu)
        {
            var listaKont = kszobApplicationService.PobierzListeIdKontWAdresu(idAdresu);

            return listaKont.Select(konto => 
                    new Egzekucje.NET.Kszob.IdKontaWAdresu(konto.IdAdresu, konto.IdSystemu, konto.IdKontaWymiarowego))
                .ToList();
        }

        public List<Kszob.SumaNaleznosciOsoby> PobierzSumyPrzeterminowanychNaleznosciOsobNaBiezacyDzien()
        {
            var naleznosci = kszobApplicationService.PobierzSumyPrzeterminowanychNaleznosciOsobNaBiezacyDzien();

            return naleznosci.Select(nal => Egzekucje.NET.Kszob.SumaNaleznosciOsoby.StworzZ(nal)).ToList();
        }
    }
}
