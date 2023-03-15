using FirebirdSql.Data.FirebirdClient;
using Infosystem.Db;
using System.Collections.Generic;
using System.Linq;

namespace Egzekucje.NET
{
    public class EfZaleglosciRepository : ZaleglosciRepository
    {
        public void SaveOrUpdate(Zaleglosc zaleglosc)
        {
            UnitOfWork<EgzekucjeDbContext>.Execute(ctx =>
            {
                if (ctx.Zaleglosci.Where(z => z.IdNaleznosci == zaleglosc.IdNaleznosci).Count() == 0)
                {
                    ctx.Zaleglosci.Add(zaleglosc);
                }
                else
                {
                    ctx.Entry(zaleglosc).State = System.Data.Entity.EntityState.Modified;
                }
                ctx.SaveChanges();
            });
        }

        public List<Zaleglosc> PobierzDlaOsoby(long idOsoby)
        {
            return UnitOfWork<EgzekucjeDbContext>.ExecuteWithResult(c =>
            {
                return c.Zaleglosci.Where(z => z.IdOsoby == idOsoby).ToList();
            });
        }

        public List<Zaleglosc> PobierzWszystkie()
        {
            return UnitOfWork<EgzekucjeDbContext>.ExecuteWithResult(c =>
            {
                return c.Zaleglosci.ToList();
            });
        }

        public void UsunPoIdOsoby(long idOsoby)
        {
            UnitOfWork<EgzekucjeDbContext>.Execute(c =>
            {
                c.Database.ExecuteSqlCommand(
                    "DELETE FROM EGZ_ZALEGLOSCI WHERE ID_OSOBY = @idOsoby",
                    new FbParameter("@idOsoby", idOsoby));
            });
        }
    }
}