using HucaresServer.Storage.Helpers;
using System.Web.Http;
using static HucaresServer.Models.CameraInfoDataModels;
using HucaresServer.Utils;
using System;
using HucaresServer.Properties;

namespace HucaresServer.Controllers
{
    /// <summary>
    /// Responsible for managing the CameraInfo database table via http requests.
    /// </summary>
    /// <seealso cref="CameraInfoHelper"/>
    public class CameraInfoController : ApiController
    {
        public ICameraInfoHelper CameraInfoHelper { get; set; } = new CameraInfoHelper();

        [HttpPost]
        [Route("api/camera/insert")]
        public IHttpActionResult InsertCamera([FromBody] InsertCameraDataModel data)
        {
            return Json(CameraInfoHelper.InsertCamera(data.HostUrl, data.Latitude, data.Longitude, data.IsTrustedSource));
        }

        [HttpPut]
        [Route("api/camera/update/source/{id}")]
        public IHttpActionResult UpdateCameraSource(int id, [FromBody] UpdateCameraSourceDataModel data)
        {
            return Json(CameraInfoHelper.UpdateCameraSource(id, data.HostUrl, data.IsTrustedSource));
        }

        [HttpPut]
        [Route("api/camera/update/activity/{id}")]
        public IHttpActionResult UpdateCameraActivity(int id, [FromBody] UpdateCameraActivityDataModel data)
        {
            return Json(CameraInfoHelper.UpdateCameraActivity(id, data.IsActive));
        }

        [HttpDelete]
        [Route("api/camera/delete/{id}")]
        public IHttpActionResult DeleteCameraById(int id)
        {
            return Json(CameraInfoHelper.DeleteCameraById(id));
        }

        [HttpGet]
        [Route("api/camera/all")]
        public IHttpActionResult GetAllCameras(bool? isTrustedSource = null)
        {
            return Json(CameraInfoHelper.GetAllCameras(isTrustedSource));
        }

        [HttpGet]
        [Route("api/camera/all/{plateNumber}")]
        public IHttpActionResult GetCamerasByPlateNumber(string plateNumber, bool? isTrustedSource = true)
        {
            return Json(CameraInfoHelper.GetCamerasByPlateNumber(plateNumber, isTrustedSource));
        }

        [HttpGet]
        [Route("api/camera/active")]
        public IHttpActionResult GetActiveCameras(bool? isTrustedSource = null)
        {
            return Json(CameraInfoHelper.GetActiveCameras(isTrustedSource));
        }

        [HttpGet]
        [Route("api/camera/inactive")]
        public IHttpActionResult GetInactiveCameras()
        {
            return Json(CameraInfoHelper.GetInactiveCameras());
        }

        [HttpGet]
        [Route("api/camera/{id}")]
        public IHttpActionResult GetCameraById(int id)
        {
            return Json(CameraInfoHelper.GetCameraById(id));
        }

        // DEMONSTRATION PURPOSES ONLY
        [HttpDelete]
        [Route("api/camera/all")]
        public IHttpActionResult DeleteCameras()
        {
            CameraInfoHelper.DeleteAll();
            return Ok();
        }
    }
}
