using System;
using System.Data.Entity;
using System.Transactions;

namespace Infosystem.Db
{
    public class UnitOfWork<T> where T : DbContext
    {
        public static void ExecuteTransactional(Action<T> operation)
        {
            using (var dbContext = (T) Activator.CreateInstance(typeof(T)))
            {
                using (var tr = dbContext.Database.BeginTransaction())
                {
                    operation.Invoke(dbContext);

                    dbContext.SaveChanges();
                    tr.Commit();
                }
            }
        }

        public static U ExecuteTransactionalWithResult<U>(Func<T, U> operation)
        {
            using (var dbContext = (T) Activator.CreateInstance(typeof(T)))
            {
                using (var tr = dbContext.Database.BeginTransaction())
                {
                    U result = operation.Invoke(dbContext);

                    dbContext.SaveChanges();
                    tr.Commit();

                    return result;
                }
            }
        }

        public static void Execute(Action<T> operation)
        {
            using (var dbContext = (T)Activator.CreateInstance(typeof(T)))
            {
                operation.Invoke(dbContext);
            }
        }

        public static U ExecuteWithResult<U>(Func<T, U> operation)
        {
            using (var dbContext = (T)Activator.CreateInstance(typeof(T)))
            {
                return operation.Invoke(dbContext);
            }
        }

        public static void InTransaction(Action operation)
        {
            using (var transaction = new TransactionScope())
            {
                operation.Invoke();
                transaction.Complete();
            }
        }
    }
}
