using Hucares.Server.Client;
using SqliteManipulation;
using SqliteManipulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var dlpList = sqlite.GetDLPs().ToList();
            var mlpList = sqlite.GetMLPs().ToList();
            var cameraList = sqlite.GetCameras().ToList();

            Task.Run(async () =>
            {
                var returnedCameraList = await PopulateCameras(cameraList);
                var joinedCameras = returnedCameraList.Join
                    (
                        cameraList,
                        newc => newc.HostUrl,
                        oldc => oldc.HostUrl,
                        (newc, oldc) => new { OldId = oldc.Id, NewId = newc.Id }
                    ).ToDictionary(c => c.OldId, c => c.NewId);

                foreach (var dlp in dlpList)
                {
                    dlp.CamId = joinedCameras[dlp.CamId];
                }

                await PopulateMLPs(mlpList);
                await PopulateDLPs(dlpList);
            }).GetAwaiter().GetResult();
            Console.ReadKey();
        }

        static async Task<List<Camera>> PopulateCameras (IEnumerable<Camera> cameras)
        {
            var resultList = new List<Camera>();

            await CameraClient.DeleteAllCameras();
            foreach (var camera in cameras)
            {
                Console.WriteLine($"Adding MLP {camera.Id}");
                DumpItem(camera);
                try {
                    resultList.Add(await CameraClient.InsertCamera(camera));
                }
                catch
                {
                    Console.WriteLine($"Add Failed!");
                }
                Console.WriteLine($"Add success!");
                Console.WriteLine();
            }

            return resultList;
        }

        static async Task PopulateMLPs(IEnumerable<MLP> mlps)
        {
            await MissingClient.DeleteAllMLPs();
            foreach (var mlp in mlps)
            {
                Console.WriteLine($"Adding MLP {mlp.Id}");
                DumpItem(mlp);
                try { 
                    await MissingClient.InsertPlateRecord(mlp.PlateNumber, mlp.SearchStartDateTime);
                }
                catch
                {
                    Console.WriteLine($"Add Failed!");
                }
            Console.WriteLine($"Add success!");
                Console.WriteLine();
            }
        }

        static async Task PopulateDLPs(IEnumerable<DLP> dlps)
        {
            await DetectedClient.DeleteAllDLPs();
            foreach (var dlp in dlps)
            {
                Console.WriteLine($"Adding DLP {dlp.Id}");
                DumpItem(dlp);
                try
                {
                    await DetectedClient.DemonstrationAddDlp(dlp);
                }
                catch
                {
                    Console.WriteLine($"Add Failed!");
                }
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
