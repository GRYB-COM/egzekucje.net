using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Egzekucje.NET.Adapters
{
    public class AdapterJsonError
    {
        public AdapterJsonError(Exception exc)
        {
            Message = exc.Message;
            StackTrace = exc.StackTrace;
            RawException = exc.ToString();
            if (exc.Data.Count > 0)
            {
                foreach (DictionaryEntry item in exc.Data)
                {
                    Data[item.Key.ToString()] = item.Value.ToString();
                }
            }
        }
        public string Message { get; private set; }
        public string StackTrace { get; private set; }
        public string RawException { get; private set; }
        public Dictionary<string, string> Data { get; private set; }
    }
    public class EgzekucjeAdapterJson
    {
        private static EgzApplicationService egzApplicationService;
        public static EgzApplicationService EgzApplicationService 
        { 
            set
            {
                if (egzApplicationService == null)
                {
                    egzApplicationService = value;
                }
            } 
        }

        public static void Initialize()
        {
            if (egzApplicationService == null)
            {
                egzApplicationService = new EgzApplicationService();
            }
        }
        public static string PobierzListeZaleglosci(long idOsoby)
        {
            if (egzApplicationService == null) throw new NullReferenceException("Biblioteka nie została poprawnie zainicjowana. Kod błędu EGZCLR0001");
            return  Newtonsoft.Json.JsonConvert.SerializeObject(egzApplicationService.PobierzZaleglosci(idOsoby));
        }

        public static string PobierzSumyZaleglosciOsobNaDzienBiezacy()
        {
            try
            {
                if (egzApplicationService == null) throw new NullReferenceException("Biblioteka nie została poprawnie zainicjowana. Kod błędu EGZCLR0001");
                return Newtonsoft.Json.JsonConvert.SerializeObject(egzApplicationService.PobierzSumyZaleglosciOsobNaDzienBiezacy());
            }
            catch (Exception exc)
            {
                 return Newtonsoft.Json.JsonConvert.SerializeObject(new AdapterJsonError(exc));
            }
        }

        public static string PobierzListeKontDlaAdresu(long idAdresu)
        {
            try
            {
                if (egzApplicationService == null) throw new NullReferenceException("Biblioteka nie została poprawnie zainicjowana. Kod błędu EGZCLR0001");
                return Newtonsoft.Json.JsonConvert.SerializeObject(egzApplicationService.PobierzListeKontDlaAdresu(idAdresu));
            }
            catch (Exception exc)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new AdapterJsonError(exc));
            }

        }

        public static string WystawUpomnienie(string jsonInput)
        {
            try
            {
                if (egzApplicationService == null) throw new NullReferenceException("Biblioteka nie została poprawnie zainicjowana. Kod błędu EGZCLR0001");
                return Adapt<WystawUpomnienieRequest>(
                               jsonInput,
                               up => egzApplicationService.WystawPojedynczeUpomnienie(up.IdOsoby, up.IdAdresu, up.Zaleglosci, up.DataUpomnienia)); 
            } catch(Exception exc)
            {
                exc.Data.Add("jsonInput", jsonInput);
                return Newtonsoft.Json.JsonConvert.SerializeObject(new AdapterJsonError(exc));
            }
        }

        public static string PrzeksztalcNaleznosciNaZaleglosciNaDzienBiezacy()
        {
            try
            {
                if (egzApplicationService == null) throw new NullReferenceException("Biblioteka nie została poprawnie zainicjowana. Kod błędu EGZCLR0001");
                egzApplicationService.ZaktualizujZaleglosciNaDanyDzien(); 
                return "SUKCES";
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }
        }

         public static string PobierzDokumentUpomnienia(long idUpomnienia)
        {
            return egzApplicationService.PobierzDokumentUpomnienia(idUpomnienia);
        }
        public static string PobierzAdresyAdresata(long idOsoby)
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(egzApplicationService.PobierzAdresyAdresata(idOsoby));
        }
        public static string PobierzDaneAdresata(long idOsoby,long idAdresu)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(egzApplicationService.PobierzDaneAdresata(idOsoby, idAdresu));
        }
        private static string Adapt<T>(string jsonInput, Func<T, object> func)
        {
            try
            {
                T request = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonInput);

                var result = func.Invoke(request);
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            catch (Exception exc)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new AdapterJsonError(exc));
            }
        }

    }
    public class WystawUpomnienieRequest
    {
        [JsonProperty("idOsoby")]
        public long IdOsoby { get; set; }
        [JsonProperty("idAdresu")]
        public long IdAdresu { get; set; }
        [JsonProperty("listaZaleglosci")]
        public List<Zaleglosc> Zaleglosci { get; set; } = new List<Zaleglosc>();
        [JsonProperty("dataUpomnienia")]
        public DateTime DataUpomnienia { get; set; }
    }

}