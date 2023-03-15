using EntityFramework.Firebird;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InfoSystemFirebirdConfig
{
    public class InfoSystemDbConfiguration : DbConfiguration
    {
        public InfoSystemDbConfiguration()
        {
            this.SetDefaultConnectionFactory(new FbConnectionFactory());
            this.SetProviderFactory(FbProviderServices.ProviderInvariantName, FirebirdSql.Data.FirebirdClient.FirebirdClientFactory.Instance);
            this.SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
            this.SetProviderServices("FirebirdSql.Data.FirebirdClient", FbProviderServices.Instance);
        }

        public static string ReadConnectionString(string dbContext, string dbConfig)
        {
            var connectionString = "";
            XDocument doc = XDocument.Load(dbConfig);
            var con = doc.Descendants("add");
            foreach (var _object in con)
            {
                if (_object.FirstAttribute.Value == dbContext)
                {
                    connectionString = _object.FirstAttribute.NextAttribute.Value;
                }
            }
            if (string.IsNullOrEmpty(connectionString)) throw new Exception(string.Format("Connection string {0} is missing in file {1}", dbContext, dbConfig));
            return connectionString;
        }
    }
}
