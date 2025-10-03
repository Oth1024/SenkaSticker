using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenkaSticker.Database
{
    public static class DatabaseClient
    {
        #region Constructor
        static DatabaseClient()
        {
            var dataRootDir = "./Data";
            var sqlFileName = "senka_sticker_data.db";
            var sqlFilePath = Path.Combine(dataRootDir, sqlFileName);
            if (!Directory.Exists(dataRootDir))
            {
                Directory.CreateDirectory(dataRootDir);
            }
            var config = new ConnectionConfig
            {
                ConnectionString = $"Data Source={sqlFilePath}",
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
            };
            _db = new SqlSugarScope(config);
            _db.DbMaintenance.CreateDatabase();
        }
        #endregion

        #region Fields
        private static SqlSugarClient _dbClient;
        private static SqlSugarScope _db;
        #endregion

        #region Properties
        public static SqlSugarScope Db => _db;
        #endregion

        #region Methods
        public static void CreateTable<TEntity>()
        {
            Db.CodeFirst
                .SplitTables()
                .InitTables<TEntity>();
        }
        #endregion
    }
}
