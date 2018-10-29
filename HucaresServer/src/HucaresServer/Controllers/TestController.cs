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

namespace HucaresServer.Controllers
{
    public class TestController : ApiController
    {
        public IDbContextFactory DbContextFactory { get; set; } = new DbContextFactory();

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
        [Route("api/test/pingAlpr")]
        public IHttpActionResult PingAlpr()
        {
            var apiInstance = new DefaultApi();
            var secretKey = "";  // string | The secret key used to authenticate your account.  You can view your  secret key by visiting  https://cloud.openalpr.com/ 
            var country = "EU";  // string | Defines the training data used by OpenALPR.  \"us\" analyzes  North-American style plates.  \"eu\" analyzes European-style plates.  This field is required if using the \"plate\" task  You may use multiple datasets by using commas between the country  codes.  For example, 'au,auwide' would analyze using both the  Australian plate styles.  A full list of supported country codes  can be found here https://github.com/openalpr/openalpr/tree/master/runtime_data/config 
            var recognizeVehicle = 0;  // int? | If set to 1, the vehicle will also be recognized in the image This requires an additional credit per request  (optional)  (default to 0)
            var state = "LT";  // string | Corresponds to a US state or EU country code used by OpenALPR pattern  recognition.  For example, using \"md\" matches US plates against the  Maryland plate patterns.  Using \"fr\" matches European plates against  the French plate patterns.  (optional)  (default to )
            var returnImage = 0;  // int? | If set to 1, the image you uploaded will be encoded in base64 and  sent back along with the response  (optional)  (default to 0)
            var topn = 5;  // int? | The number of results you would like to be returned for plate  candidates and vehicle classifications  (optional)  (default to 10)
            var prewarp = "";  // string | Prewarp configuration is used to calibrate the analyses for the  angle of a particular camera.  More information is available here http://doc.openalpr.com/accuracy_improvements.html#calibration  (optional)  (default to )

            InlineResponse200 result;
            
            try
            {
                
                using (Image image = Image.FromFile("test.jpg"))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        // Convert byte[] to Base64 String
                        string base64String = Convert.ToBase64String(imageBytes);

                        return Json(base64String);
                        
                        result = apiInstance.RecognizeBytes(base64String, secretKey, country, recognizeVehicle, state, returnImage, topn, prewarp);
                        Debug.WriteLine(result);
                    }
                }
            }
            catch (Exception e)
            {
                return Json("Exception when calling DefaultApi.RecognizeBytes: " + e.Message );
            }
            
            return Json(result);
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
