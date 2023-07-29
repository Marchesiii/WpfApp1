using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class PostgresConnection : IDbConnection, IDisposable
    {
        public NpgsqlConnection Connection { get; set; }

        public PostgresConnection()
        {
            Connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User id=postgres;Password=123;");
            Connection.Open();
        }

        public void Close()
        {
            Connection.Close();
            Dispose();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        public DbConnection GetConnection() => Connection;
    }
}
