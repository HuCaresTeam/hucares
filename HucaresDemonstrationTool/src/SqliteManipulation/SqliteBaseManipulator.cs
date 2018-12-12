using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using SqliteManipulation.Models;
using System.Data;

namespace SqliteManipulation
{
    public abstract class SqliteBaseManipulator
    {
        protected const string connectionString = @"Data Source=SqliteData\HucaresMock.sqlite;Version=3;";

        public IEnumerable<Target> GetData<Target>(string query) where Target : new()
        {
            using (var sqliteConn = new SQLiteConnection(connectionString))
            {
                sqliteConn.Open();
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, sqliteConn))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, typeof(Target).Name);
                    return ds.MapToObjectEnumerable<Target>();
                }
            }
        }
    }
}
