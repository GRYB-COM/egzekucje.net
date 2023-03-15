using BOS.NET;
using System.Collections.Generic;

namespace Egzekucje.NET.Bos
{
    public interface BosService
    {
        Adresat PobierzDaneAdresata(long idOsoby, long idAdresu);
        List<Adres> PobierzAdresyAdresata(long idOsoby);
    }
}