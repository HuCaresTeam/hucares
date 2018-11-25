using HucaresServer.DataAcquisition;
using OpenAlprApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HucaresServer.Storage.Helpers;

namespace HucaresServer.TimedProcess
{
    public class DlpCollectionProcess
    {
        private readonly ICameraImageDownloading _cameraImageDownloading;
        private readonly IOpenAlprWrapper _openAlprWrapper;
        private readonly IDetectedPlateHelper _dlpHelper;
        private readonly IImageSaver _imageSaver;

        public DlpCollectionProcess(ICameraImageDownloading cameraImageDownloading = null, 
                                    IOpenAlprWrapper openAlprWrapper = null, 
                                    IDetectedPlateHelper dlpHelper = null,
                                    IImageSaver imageSaver = null)
        {
            _cameraImageDownloading = cameraImageDownloading ?? new CameraImageDownloading();
            _openAlprWrapper = openAlprWrapper ?? new OpenAlprWrapper();
            _dlpHelper = dlpHelper ?? new DetectedPlateHelper();
            _imageSaver = imageSaver ?? new LocalImageSaver();
        }

        public async Task StartProccess()
        {
            //Downloads images and saves to temp folder
            var dateNow = DateTime.Now;
            await _cameraImageDownloading.DownloadImagesFromCameraInfoSources(true, dateNow);

            //Sends all images to API and puts results in task list
            var fileToTaskMap = new Dictionary<FileInfo, Task<InlineResponse200>>();
            foreach (var file in _imageSaver.GetTempFiles())
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
                var resultList = (await fileTaskPair.Value).Results;

                string newFileLocation = null;
                int camId = 0;
                if (0 != resultList.Count)
                {
                    var file = fileTaskPair.Key;

                    newFileLocation = _imageSaver.MoveFileToPerm(file);
                    camId = _imageSaver.ExtractCameraId(file);
                }

                foreach (var result in resultList)
                {
                    var confidenceResult = result.Confidence ?? 0;
                    _dlpHelper.InsertNewDetectedPlate(result.Plate, dateNow, camId, newFileLocation, decimal.ToDouble(confidenceResult));
                }
            }
        }
    }
}
