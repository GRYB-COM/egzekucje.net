using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egzekucje.NET
{
    public class SumaZaleglosciOsoby
    {
 
        public long IdOsoby { get; private set; }
        public string Pesel { get; private set; }
        public string Regon { get; private set; }
        public string Nazwa { get; private set; }
        public decimal Zaleglosc { get; private set; }
        public decimal Odsetki { get; private set; }

        public static SumaZaleglosciOsoby StworzZ(Kszob.SumaNaleznosciOsoby sumaNaleznosciOsoby)
        {
            return new SumaZaleglosciOsoby 
            {
                IdOsoby = sumaNaleznosciOsoby.IdOsoby,
                Pesel = sumaNaleznosciOsoby.Pesel,
                Regon = sumaNaleznosciOsoby.Regon,
                Nazwa = sumaNaleznosciOsoby.Nazwa,
                Zaleglosc = sumaNaleznosciOsoby.Zaleglosc,
                Odsetki = sumaNaleznosciOsoby.Odsetki
                
            };
        }

    }
}
