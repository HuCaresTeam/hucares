using Hucares.Server.Client;
using SqliteManipulation;
using SqliteManipulation.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HucaresDemonstrationTool
{
    class Program
    {

        private static CameraInfoClient CameraClient { get; set; } = new CameraInfoClient();
        private static DetectedPlateClient DetectedClient { get; set; } = new DetectedPlateClient();
        private static MissingPlateClient MissingClient { get; set; } = new MissingPlateClient();

        static void Main(string[] args)
        {
            var sqlite = new SqliteHucaresManipulator();

            if (args.Length > 0)
            {
                DateTime newDateTime;
                if ("NOW" == args[0].ToUpper())
                {
                    newDateTime = DateTime.Now;
                }
                else
                {
                    newDateTime = DateTime.Parse(args[0]);
                }
                sqlite.UpdateDlpDateTimes(newDateTime);
            }

            var dlpList = sqlite.GetDLPs();
            var mlpList = sqlite.GetMLPs();
            var cameraList = sqlite.GetCameras();

            DumpEnumerable(dlpList);
            // DumpEnumerable(mlpList);
            // DumpEnumerable(cameraList);

            Task.Run(async () =>
            {
                await PopulateCameras(cameraList);
                await PopulateMLps(mlpList);
            }).GetAwaiter().GetResult();
            Console.ReadKey();
        }

        static async Task PopulateCameras (IEnumerable<Camera> cameras)
        {
            await CameraClient.DeleteAllCameras();
            foreach (var camera in cameras)
            {
                Console.WriteLine($"Adding MLP {camera.Id}");
                DumpItem(camera);
                await CameraClient.InsertCamera(camera);
                Console.WriteLine($"Add success!");
                Console.WriteLine();
            }
        }

        static async Task PopulateMLps(IEnumerable<MLP> mlps)
        {
            await MissingClient.DeleteAllMLPs();
            foreach (var mlp in mlps)
            {
                Console.WriteLine($"Adding MLP {mlp.Id}");
                DumpItem(mlp);
                await MissingClient.InsertPlateRecord(mlp.PlateNumber, mlp.SearchStartDateTime);
                Console.WriteLine($"Add success!");
                Console.WriteLine();
            }
        }

        static void DumpEnumerable<T>(IEnumerable<T> enumerable)
        {
            var itemType = typeof(T);
            Console.WriteLine($"Dump of {itemType.Name}");
            foreach (T item in enumerable)
            {
                DumpItem(item);
                Console.WriteLine();
            }
        }

        static void DumpItem<T>(T item)
        {
            var itemType = typeof(T);
            var properties = itemType.GetProperties();
            foreach (var propertyInfo in properties)
            {
                Console.WriteLine($"\t{propertyInfo.Name} - {propertyInfo.GetValue(item)}");
            }
        }
    }
}
