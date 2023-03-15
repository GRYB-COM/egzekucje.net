using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egzekucje.NET.Kszob
{
    public class SumaNaleznosciOsoby
    {
 
        public long IdOsoby { get; private set; }
        public string Pesel { get; private set; }
        public string Regon { get; private set; }
        public string Nazwa { get; private set; }
        public decimal Zaleglosc { get; private set; }
        public decimal Odsetki { get; private set; }

        public static Egzekucje.NET.Kszob.SumaNaleznosciOsoby StworzZ(global::Kszob.NET.SumaNaleznosciOsoby zal)
        {
            return new Egzekucje.NET.Kszob.SumaNaleznosciOsoby 
            {
                IdOsoby = zal.idOsoby,
                Pesel = zal.pesel,
                Regon = zal.regon,
                Nazwa = zal.nazwa,
                Zaleglosc = zal.zaleglosc,
                Odsetki = zal.odsetki
                
            };
        }

    }
}
