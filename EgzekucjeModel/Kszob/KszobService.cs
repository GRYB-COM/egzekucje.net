using Kszob.NET;
using System.Collections.Generic;

namespace Egzekucje.NET.Kszob
{
    public interface KszobService
    {
        List<Naleznosc> PobierzPrzeterminowaneNaleznosciOsoby(long idOsoby);

        List<Naleznosc> PobierzPrzeterminowaneNaleznosciNaBiezacyDzien();

        List<IdKontaWAdresu> PobierzListeKontDlaAdresu(long idAdresu);
        List<SumaNaleznosciOsoby> PobierzSumyPrzeterminowanychNaleznosciOsobNaBiezacyDzien();
    }
}