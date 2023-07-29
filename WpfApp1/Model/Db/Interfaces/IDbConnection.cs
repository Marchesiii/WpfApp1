using System;
using System.Data.Common;

namespace WpfApp1

{
    public interface IDbConnection
    {
        public void Close();
        public DbConnection GetConnection();

    }
}
