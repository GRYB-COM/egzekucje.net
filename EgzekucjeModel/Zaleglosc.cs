using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Egzekucje.NET
{
    [Table("EGZ_ZALEGLOSCI")]
    public class Zaleglosc
    {
        public Zaleglosc() {}

        [Key]
        [Column("ID_ZALEGLOSCI")]
        [JsonProperty("ID_ZALEGLOSCI")] 
        public long IdZaleglosci { get; set; }

        [Column("ID_NALEZNOSCI")]
        [JsonProperty("ID_NALEZNOSCI")]
        public long IdNaleznosci { get; set; }

        // IS_ZOBOW
        [Column("ID_ZOBOWIAZANIA")]
        [JsonProperty("ID_ZOBOWIAZANIA")]
        public long IdZobowiazania { get; set; }
        
        // IS_REJESTR
        [Column("ID_SYSTEMU_WYMIAROWEGO")]
        [JsonProperty("ID_SYSTEMU_WYMIAROWEGO")]
        public long IdSystemuWymiarowego { get; set; }

        [Column("ID_KONTA_WYMIAROWEGO")]
        [JsonProperty("ID_KONTA_WYMIAROWEGO")]
        public long IdKontaWymiarowego { get; set; }

        [Column("ID_OSOBY")]
        [JsonProperty("ID_OSOBY")]
        public long IdOsoby { get; set; }

        [Column("ROK_OBROTOWY")]
        [JsonProperty("ROK_OBROTOWY")]
        public int RokObrotowy { get; set; }

        [Column("RATA")]
        [JsonProperty("RATA")]
        public decimal Rata { get; set; }

        [Column("ID_RATY")]
        [JsonProperty("ID_RATY")]
        public long IdRaty { get; set; }

        [Column("KWOTA_ZALEGLOSCI")]
        [JsonProperty("KWOTA_ZALEGLOSCI")]
        public decimal KwotaZaleglosci { get; set; }

        [Column("KWOTA_ODSETEK")]
        [JsonProperty("KWOTA_ODSETEK")]
        public decimal KwotaOdsetek { get; set; }

        [Column("TERMIN_PLATNOSCI")]
        [JsonProperty("TERMIN_PLATNOSCI")]
        public DateTime TerminPlatnosci { get; set; }

        [Column("STAWKA_VAT")]
        [JsonProperty("STAWKA_VAT")]
        public string StawkaVAT { get; set; }

        [Column("ID_UPOMNIENIA")]
        [ForeignKey("Upomnienie")]
        [JsonProperty("ID_UPOMNIENIA")]
        public long? UpomnienieId { get; set; }

        [JsonIgnore]
        public Upomnienie Upomnienie { get; set; }
    }
}