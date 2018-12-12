using SqliteManipulation.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace SqliteManipulation
{
    public class SqliteHucaresManipulator : SqliteBaseManipulator
    {
        public IEnumerable<DLP> GetDLPs()
        {
            return GetData<DLP>("SELECT * FROM DLP");
        }

        public IEnumerable<Camera> GetCameras()
        {
            return GetData<Camera>("SELECT * FROM Camera");
        }

        public IEnumerable<MLP> GetMLPs()
        {
            return GetData<MLP>("SELECT * FROM MLP");
        }

        public IEnumerable<DLP> UpdateDlpDateTimes(DateTime newDateTime)
        {
            using (var sqliteConn = new SQLiteConnection(connectionString))
            {
                sqliteConn.Open();
                var updateCommand = new SQLiteCommand("UPDATE DLP SET DetectedDateTime = @DDT WHERE Id = @Id", sqliteConn);
                updateCommand.Parameters.Add("@DDT", DbType.String, 50, "DetectedDateTime");
                updateCommand.Parameters.Add("@Id", DbType.Int64, 8, "Id");

                var da = new SQLiteDataAdapter("SELECT Id, DetectedDateTime FROM DLP ORDER BY DetectedDateTime DESC;", sqliteConn);
                da.UpdateCommand = updateCommand;

                DataSet ds = new DataSet();
                da.Fill(ds, "DLP");

                var dlpTable = ds.Tables[0];
                var timeSpan = newDateTime - dlpTable.Rows[0]["DetectedDateTime"].ToString().ToIsoDateTime();

                foreach (DataRow row in dlpTable.Rows)
                {
                    var parsedDatetime = row["DetectedDateTime"].ToString().ToIsoDateTime();
                    row["DetectedDateTime"] = (parsedDatetime + timeSpan).ToIsoDateTimeString();
                }

                da.Update(dlpTable);

                da.Dispose();
                return ds.MapToObjectEnumerable<DLP>();
            }
        }
    }
}
