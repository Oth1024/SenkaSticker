using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenkaSticker.Database
{
    public abstract class RepositoryBase<TEntity>
    {
        #region Constructor
        protected RepositoryBase()
        {
            DatabaseClient.CreateTable<TEntity>();
        }
        #endregion

        #region Fields
        protected SqlSugarScope _db => DatabaseClient.Db;
        #endregion
    }
}
