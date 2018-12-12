using SqliteManipulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
