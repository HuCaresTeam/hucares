using HucaresServer.Storage.Helpers;
using System.Web.Http;
using static HucaresServer.Models.CameraInfoDataModels;

namespace HucaresServer.Controllers
{
    public class CameraInfoController : ApiController
    {
        public CameraInfoHelper CameraInfoHelper { get; set; } = new CameraInfoHelper();

        [HttpPost]
        [Route("api/camera/insert")]
        public IHttpActionResult InsertCamera([FromBody] InsertCameraDataModel data)
        {
            return Json(CameraInfoHelper.InsertCamera(data.HostUrl, data.Latitude, data.Longitude, data.IsTrustedSource));
        }

        [HttpPost]
        [Route("api/camera/update/source/{id}")]
        public IHttpActionResult UpdateCameraSource(int id, [FromBody] UpdateCameraSourceDataModel data)
        {
            return Json(CameraInfoHelper.UpdateCameraSource(id, data.HostUrl, data.IsTrustedSource));
        }

        [HttpPost]
        [Route("api/camera/update/activity/{id}")]
        public IHttpActionResult UpdateCameraActivity(int id, [FromBody] bool isActive)
        {
            return Json(CameraInfoHelper.UpdateCameraActivity(id, isActive));
        }

        [HttpPost]
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
    }
}
