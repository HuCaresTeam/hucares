using HucaresServer.DataAcquisition;
using OpenAlprApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HucaresServer.Storage.Helpers;
using System.Text.RegularExpressions;

namespace HucaresServer.TimedProcess
{
    public class DlpCollectionProcess
    {
        // TODO: Take this value from a configuration file or somewhere else
        private const string TemporaryStorageUrl = "temporaryImages";
        private const string PermanentStorageUrl = "images";

        private readonly ICameraImageDownloading _cameraImageDownloading;
        private readonly IOpenAlprWrapper _openAlprWrapper;
        private readonly IDetectedPlateHelper _dlpHelper;

        public DlpCollectionProcess(ICameraImageDownloading cameraImageDownloading = null, 
                                    IOpenAlprWrapper openAlprWrapper = null, 
                                    IDetectedPlateHelper dlpHelper = null)
        {
            _cameraImageDownloading = cameraImageDownloading ?? new CameraImageDownloading();
            _openAlprWrapper = openAlprWrapper ?? new OpenAlprWrapper();
            _dlpHelper = dlpHelper ?? new DetectedPlateHelper();
        }

        public async Task StartProccess()
        {
            var dateNow = DateTime.Now;
            await _cameraImageDownloading.DownloadImagesFromCameraInfoSources(true, dateNow);

            DirectoryInfo dir = new DirectoryInfo(GetTempStorage());

            var fileToTaskMap = new Dictionary<FileInfo, Task<InlineResponse200>>();
            foreach (var file in dir.GetFiles())
            {
                fileToTaskMap.Add(file, _openAlprWrapper.DetectPlateAsync(file.FullName));
            }

            await ProcessResults(dateNow, fileToTaskMap);
        }

        private async Task ProcessResults(DateTime dateNow, Dictionary<FileInfo, Task<InlineResponse200>> fileToTaskMap)
        {
            await Task.WhenAll(fileToTaskMap.Values);

            foreach (var fileTaskPair in fileToTaskMap)
            {
                var inlineResponse = await fileTaskPair.Value;
                var resultList = inlineResponse.Results;

                string newFileLocation = null;
                int camId = 0;
                if (0 != resultList.Count)
                {
                    var file = fileTaskPair.Key;

                    newFileLocation = MoveFileToPerm(file);
                    camId = ExtractCameraId(file);
                }

                foreach (var result in inlineResponse.Results)
                {
                    var confidenceResult = result.Confidence ?? 0;
                    _dlpHelper.InsertNewDetectedPlate(result.Plate, dateNow, camId, newFileLocation, decimal.ToDouble(confidenceResult));
                }
            }
        }

        private int ExtractCameraId(FileInfo file)
        {
            var fileCamIdMatch = Regex.Match(file.Name, @"^(\d+)_.+");
            if (!fileCamIdMatch.Success)
            {
                throw new Exception("Bad file name");
            }

            int.TryParse(fileCamIdMatch.Groups[1].Value, out int camId);
            return camId;
        }

        private string GetTempStorage()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), TemporaryStorageUrl);
        }
    }
}
