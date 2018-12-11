using SqliteManipulation;
using SqliteManipulation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresDemonstrationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqlite = new SqliteDataGetter();

            var dlpList = sqlite.GetData<DLP>("SELECT * FROM DLP");
            var mlpList = sqlite.GetData<MLP>("SELECT * FROM MLP");
            var cameraList = sqlite.GetData<Camera>("SELECT * FROM Camera");

            Dump(dlpList);
            Dump(mlpList);
            Dump(cameraList);

            Console.ReadKey();
        }

        static void Dump<T>(IEnumerable<T> enumerable)
        {
            var itemType = typeof(T);
            Console.WriteLine($"Dump of {itemType.Name}");
            var properties = itemType.GetProperties();
            foreach (var item in enumerable)
            {
                foreach(var propertyInfo in properties)
                {
                    Console.WriteLine($"\t{propertyInfo.Name} - {propertyInfo.GetValue(item)}");
                }
                Console.WriteLine();
            }
        }
    }
}
