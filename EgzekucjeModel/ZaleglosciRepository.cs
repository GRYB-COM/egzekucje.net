using System.Collections.Generic;

namespace Egzekucje.NET
{
    public interface ZaleglosciRepository
    {
        List<Zaleglosc> PobierzDlaOsoby(long idOsoby);
        void SaveOrUpdate(Zaleglosc zaleglosc);
        void UsunPoIdOsoby(long idOsoby);
        List<Zaleglosc> PobierzWszystkie();
    }
}