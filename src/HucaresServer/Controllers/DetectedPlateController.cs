﻿using HucaresServer.Storage.Helpers;
using System;
using System.Web.Http;
using static HucaresServer.Models.CameraInfoDataModels;

namespace HucaresServer.Controllers
{
    /// <summary>
    /// Responsible for managing the DetectedPlateController database table via http requests.
    /// </summary>
    /// <seealso cref="IDetectedPlateHelper"/>
    public class DetectedPlateController : ApiController
    {
        public IDetectedPlateHelper DetectedPlateHelper { get; set; } = new DetectedPlateHelper();

        [HttpGet]
        [Route("api/dlp/all")]
        public IHttpActionResult GetAllDetectedMissingPlates()
        {
            return Json(DetectedPlateHelper.GetAllDetectedMissingPlates());
        }

        [HttpGet]
        [Route("api/dlp/plate/{plateNumber}")]
        public IHttpActionResult GetAllDetectedPlatesByPlateNumber(string plateNumber, DateTime? startDateTime = null,
            DateTime? endDateTime = null)
        {
            return Json(DetectedPlateHelper.GetAllDetectedPlatesByPlateNumber(plateNumber, startDateTime, endDateTime));
        }

        [HttpGet]
        [Route("api/dlp/cam/{cameraId}")]
        public IHttpActionResult GetAllDetectedPlatesByCamera(int cameraId, DateTime? startDateTime = null, 
            DateTime? endDateTime = null)
        {
            return Json(DetectedPlateHelper.GetAllDetectedPlatesByCamera(cameraId, startDateTime, endDateTime));
        }
    }
}