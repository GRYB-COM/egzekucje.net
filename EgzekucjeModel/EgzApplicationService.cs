using BOS.NET;
using Egzekucje.NET.Adapters;
using Egzekucje.NET.Bos;
using Egzekucje.NET.Kszob;
using Infosystem.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using EgzekucjeModel.CodeGenerator;

namespace Egzekucje.NET
{
    // TODO introduce dependency injection
    // TODO introduce transaction handling
    [ApplicationService]
    public class EgzApplicationService
    {
        private UpomnieniaRepository upomnieniaRepository;
        private ZaleglosciRepository zaleglosciRepository;
        private KszobService kszobService;
        private BosService bosService;

        public EgzApplicationService(UpomnieniaRepository upomnieniaRepository, ZaleglosciRepository zaleglosciRepository, KszobService kszobService, BosService bosService)
        {
            this.upomnieniaRepository = upomnieniaRepository;
            this.zaleglosciRepository = zaleglosciRepository;
            this.kszobService = kszobService;
            this.bosService = bosService;
        }

        public EgzApplicationService()
        {
            this.upomnieniaRepository = new EfUpomnieniaRepository();
            this.zaleglosciRepository = new EfZaleglosciRepository();
            this.kszobService = new LocalKszobService();
            this.bosService = new LocalBosService();
        }

        public virtual void PrzeksztalcNaleznosciNaZaleglosci(long idOsoby)
        {
            UnitOfWork<EgzekucjeDbContext>.InTransaction(() =>
            {
                var naleznosci = kszobService.PobierzPrzeterminowaneNaleznosciOsoby(idOsoby);
                naleznosci.ForEach(nal => zaleglosciRepository.SaveOrUpdate(nal.PrzeksztalcWZaleglosc()));
            });
        }

        public virtual void ZaktualizujZaleglosciNaDanyDzien()
        {
            UnitOfWork<EgzekucjeDbContext>.InTransaction(() =>
            {
                var naleznosci = kszobService.PobierzPrzeterminowaneNaleznosciNaBiezacyDzien();
                naleznosci.ForEach(nal => zaleglosciRepository.SaveOrUpdate(nal.PrzeksztalcWZaleglosc()));
            });
        }

        public virtual List<SumaZaleglosciOsoby> PobierzSumyZaleglosciOsobNaDzienBiezacy() 
        {
            var sumaNaleznosciOsob = kszobService.PobierzSumyPrzeterminowanychNaleznosciOsobNaBiezacyDzien();
            var sumaZaleglosciOsob = new List<SumaZaleglosciOsoby>();
            sumaNaleznosciOsob.ForEach(sumaNaleznosciOsoby => sumaZaleglosciOsob.Add(SumaZaleglosciOsoby.StworzZ(sumaNaleznosciOsoby)));
            return sumaZaleglosciOsob;
        }


        public virtual List<Zaleglosc> PobierzZaleglosci(long idOsoby)
        {
            return zaleglosciRepository.PobierzDlaOsoby(idOsoby);
        }

        public List<Zaleglosc> PobierzWszystkieZaleglosci()
        {
            return zaleglosciRepository.PobierzWszystkie();
        }

        public List<Adres> PobierzAdresyAdresata(long idOsoby)
        {
            return bosService.PobierzAdresyAdresata(idOsoby);
        }

        public Adresat PobierzDaneAdresata(long idOsoby, long idAdresu)
        {
            return bosService.PobierzDaneAdresata(idOsoby,idAdresu);
        }

        public virtual Upomnienie PobierzUpomnienie(long idUpomnienia)
        {
            return upomnieniaRepository.Pobierz(idUpomnienia);
        }

        public virtual Upomnienie WystawPojedynczeUpomnienie(long idOsoby, long idAdresu, List<Zaleglosc> zaleglosci, DateTime dataUpomnienia)
        {
            Adresat adresat = bosService.PobierzDaneAdresata(idOsoby, idAdresu);
            Upomnienie upomnienie = new Upomnienie(adresat, zaleglosci, dataUpomnienia);
            upomnieniaRepository.Zapisz(upomnienie);

            return upomnienie;
        }

        public virtual string PobierzDokumentUpomnienia(long idUpomnienia)
        {
            Upomnienie upomnienie = upomnieniaRepository.Pobierz(idUpomnienia);

            return upomnienie.PobierzRtf();
        }

        public virtual List<IdKontaWAdresu> PobierzListeKontDlaAdresu(long idAdresu)
        {
            return kszobService.PobierzListeKontDlaAdresu(idAdresu);
        }
    }
}

