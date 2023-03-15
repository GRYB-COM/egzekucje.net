using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egzekucje.NET.Kszob
{
    public class IdKontaWAdresu
    {
        public IdKontaWAdresu() { }
        public IdKontaWAdresu(long idAdresu,long idSystemu,long idKontaW) 
        {
            this.IdAdresu = idAdresu;
            this.IdSystemu = idSystemu;
            this.IdKontaW = idKontaW;
        }
        // SMELL to nie bardzo json naming standard :/
        [JsonProperty("ID_SYSTEMU")]
        public long IdSystemu { get; set; }
        [JsonProperty("ID_KONTA_W")]
        public long IdKontaW { get; set; }
        [JsonProperty("ID_ADRESU")]
        public long IdAdresu { get; set; }
    }
}
