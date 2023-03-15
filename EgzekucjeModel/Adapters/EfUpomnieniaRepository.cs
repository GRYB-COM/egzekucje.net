using FirebirdSql.Data.FirebirdClient;
using Infosystem.Db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Egzekucje.NET.Adapters
{
    public class EfUpomnieniaRepository : UpomnieniaRepository
    {
        public void UsunPoIdOsoby(long idOsoby)
        {
            UnitOfWork<EgzekucjeDbContext>.Execute(c =>
            {
                c.Database.ExecuteSqlCommand(
                    "DELETE FROM EGZ_UPOMNIENIA WHERE ADRESAT_ID_OSOBY = @idOsoby",
                    new FbParameter("@idOsoby", idOsoby));
            });
        }

        public Upomnienie Zapisz(Upomnienie upomnienie)
        {
            UnitOfWork<EgzekucjeDbContext>.Execute(c =>
            {
                upomnienie.Zaleglosci.ForEach(z => c.Entry(z).State = EntityState.Modified);
                c.Upomnienia.Add(upomnienie);
                c.SaveChanges();
            });

            return upomnienie;
        }

        public Upomnienie Pobierz(long idUpomnienia)
        {
            return UnitOfWork<EgzekucjeDbContext>.ExecuteWithResult(c =>
            {
                return c.Upomnienia.Include(u => u.Zaleglosci)
                    .Where(u => u.IdUpomnienia == idUpomnienia).Single();
            });
        }
    }
}