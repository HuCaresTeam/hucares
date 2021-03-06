﻿using HucaresServer.DataAcquisition;
using OpenAlprApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HucaresServer.Storage.Helpers;
using HucaresServer.TimedProccess;

namespace HucaresServer.TimedProcess
{
    public class DlpCollectionProcess
    {
        private readonly ICameraImageDownloading _cameraImageDownloading;
        private readonly IOpenAlprWrapper _openAlprWrapper;
        private readonly IDetectedPlateHelper _dlpHelper;
        private readonly IImageFileNamer _fileNamer;
        private readonly IImageManipulator _imageManipulator;
        private readonly ILocationToUrlConverter _locationToUrl;

        public DlpCollectionProcess()
        {
            _cameraImageDownloading = new CameraImageDownloading();
            _openAlprWrapper = new OpenAlprWrapper();
            _dlpHelper =  new DetectedPlateHelper();
            _fileNamer = new ImageFileNamer();
            _imageManipulator = new LocalImageManipulator(_fileNamer);
            _locationToUrl = new LocationToUrlConverter();
        }

        public DlpCollectionProcess(ICameraImageDownloading cameraImageDownloading = null, 
                                    IOpenAlprWrapper openAlprWrapper = null, 
                                    IDetectedPlateHelper dlpHelper = null,
                                    IImageFileNamer fileNamer = null,
                                    IImageManipulator imageSaver = null, 
                                    ILocationToUrlConverter locationToUrl = null)
        {
            _cameraImageDownloading = cameraImageDownloading ?? new CameraImageDownloading();
            _openAlprWrapper = openAlprWrapper ?? new OpenAlprWrapper();
            _dlpHelper = dlpHelper ?? new DetectedPlateHelper();
            _fileNamer = fileNamer ?? new ImageFileNamer();
            _imageManipulator = imageSaver ?? new LocalImageManipulator(fileNamer);
            _locationToUrl = locationToUrl ?? new LocationToUrlConverter();
        }

        public async Task StartProccess()
        {
            //Downloads images and saves to temp folder
            var dateNow = DateTime.Now;
            try
            {
                await _cameraImageDownloading.DownloadImagesFromCameraInfoSources(true, dateNow);

                //Sends all images to API and puts results in task list
                var fileToTaskMap = new Dictionary<FileSystemInfo, Task<InlineResponse200>>();
                var images = _imageManipulator.GetTempFiles();
                foreach (var file in images)
                {
                    fileToTaskMap.Add(file, _openAlprWrapper.DetectPlateAsync(file.FullName));
                }

                await ProcessResults(dateNow, fileToTaskMap);
            }
            finally
            {
                _imageManipulator.DeleteTempFiles();
            }
        }

        private async Task ProcessResults(DateTime dateNow, Dictionary<FileSystemInfo, Task<InlineResponse200>> fileToTaskMap)
        {
            await Task.WhenAll(fileToTaskMap.Values);

            foreach (var fileTaskPair in fileToTaskMap)
            {
                var resultList = (await fileTaskPair.Value).Results;

                string newFileLocation = null;
                int camId = 0;
                var file = fileTaskPair.Key;
                if (0 != resultList.Count)
                {
                    newFileLocation = _imageManipulator.MoveFileToPerm(file, dateNow);
                    camId = _fileNamer.ExtractCameraId(file.Name);
                }

                foreach (var result in resultList)
                {
                    var confidenceResult = result.Confidence ?? 0;
                    var fileApiPath = _locationToUrl.ConvertPathToUrl(file.Name, dateNow);

                    try
                    {
                        _dlpHelper.InsertNewDetectedPlate(result.Plate, dateNow, camId, fileApiPath, decimal.ToDouble(confidenceResult));
                    }
                    catch
                    {
                        //Skip if error occurs while trying to store image
                        continue;
                    }
                }
            }
        }
    }
}
