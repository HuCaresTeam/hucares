using HucaresServer.Storage;
using HucaresServer.Storage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Http;
using HucaresServer.Storage.Helpers;
using OpenAlprApi.Api;
using OpenAlprApi.Model;
using HucaresServer.DataAcquisition;

namespace HucaresServer.Controllers
{
    public class TestController : ApiController
    {
        public IDbContextFactory DbContextFactory { get; set; } = new DbContextFactory();
        public IOpenAlprWrapper OpenAlprWrapper { get; set; } = new OpenAlprWrapper();


        [HttpGet]
        [Route("api/test/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Json($"Hello world {id}");
        }
        
        
        [HttpGet]
        [Route("api/test/generate/mlp")]
        public IHttpActionResult GenerateFakeMLPData()
        {
            var missingPlateHelper = new MissingPlateHelper();
            
            missingPlateHelper.InsertPlateRecord("JEZ123", new DateTime(2018, 10, 16));
            missingPlateHelper.InsertPlateRecord("ABG696", new DateTime(2018, 10, 20));
            missingPlateHelper.InsertPlateRecord("BDI911", new DateTime(2018, 10, 24));
            missingPlateHelper.InsertPlateRecord("HAH666", new DateTime(2018, 10, 25));
            
            return Json("Fake MLP data generated");
        }
        
        [HttpGet]
        [Route("api/test/generate/dlp")]
        public IHttpActionResult GenerateFakeDLPData()
        {
            var detectedPlateHelper = new DetectedPlateHelper();

            detectedPlateHelper.InsertNewDetectedPlate("JEZ123", new DateTime(2018, 10, 22), 1, "ipfs://hucares.io/detections/camera/28/20181022", 0.86);
            detectedPlateHelper.InsertNewDetectedPlate("JEZ123", new DateTime(2018, 10, 22), 2, "ipfs://hucares.io/detections/camera/23/20181022", 0.92);
            detectedPlateHelper.InsertNewDetectedPlate("JEZ123", new DateTime(2018, 10, 22), 1, "ipfs://hucares.io/detections/camera/28/20181022", 0.73);
            
            detectedPlateHelper.InsertNewDetectedPlate("BDI911", new DateTime(2018, 10, 20), 1, "ipfs://hucares.io/detections/camera/28/20181022", 0.71);
            detectedPlateHelper.InsertNewDetectedPlate("BDI911", new DateTime(2018, 10, 21), 1, "ipfs://hucares.io/detections/camera/28/20181022", 0.98);
            detectedPlateHelper.InsertNewDetectedPlate("BDI911", new DateTime(2018, 10, 22), 1, "ipfs://hucares.io/detections/camera/28/20181022", 0.95);
            detectedPlateHelper.InsertNewDetectedPlate("BDI911", new DateTime(2018, 10, 23), 2, "ipfs://hucares.io/detections/camera/23/20181022", 0.78);
            
            detectedPlateHelper.InsertNewDetectedPlate("HAH666", new DateTime(2018, 10, 25), 2, "ipfs://hucares.io/detections/camera/23/20181022", 0.999);
            
            return Json("Fake DLP data generated");
        }
        
        [HttpGet]
        [Route("api/test/generate/camera")]
        public IHttpActionResult GenerateFakeCameraData()
        {
            var cameraInfoHelper = new CameraInfoHelper();

            cameraInfoHelper.InsertCamera("http://map.sviesoforai.lt/camera/api/camera/Camera_028.jpg", 54.736062,
                25.264113, true);
            cameraInfoHelper.InsertCamera("http://map.sviesoforai.lt/camera/api/camera/Camera_023.jpg", 54.701329, 
                25.262231, true);
 
            return Json("Fake Camera data generated");
        }
        
        
        [HttpGet]
        [Route("api/test/detectPlate")]
        public IHttpActionResult DetectPlate()
        {
            var pathToPlateImage = "";
            InlineResponse200 response;

            try
            {
                response = OpenAlprWrapper.DetectPlate(pathToPlateImage);
            }
            catch (Exception e)
            {
                return Json($"Exception when calling OpenAlprWrapper: {e.Message}" );
            }
            
            return Json(response);
        }

        /// <summary>
        /// Example use case of accessing the DB using EntityFramework.
        /// Takes the given paramaters and stores them in the db.
        /// </summary>
        /// <param name="mlpParams"> 
        /// Contains the plate number of the missing license plate as well as DateTime of when it went missing.
        /// </param>
        /// <returns> Json representation of the value stored in the DB </returns>
        [HttpPost]
        [Route("api/test")]
        public IHttpActionResult Post([FromBody] MLPPostParams mlpParams)
        {
            var mlp = new MissingLicensePlate()
            {
                PlateNumber = mlpParams.PlateNumber,
                SearchStartDateTime = mlpParams.SearchStartDateTime
            };
            using (var ctx = DbContextFactory.BuildHucaresContext())
            {
                ctx.MissingLicensePlates.Add(mlp);
                ctx.SaveChanges();
            }
            return Json(mlp);
        }
    }

    public class MLPPostParams
    {
        public string PlateNumber { get; set; }
        public DateTime SearchStartDateTime { get; set; }
    }
}
