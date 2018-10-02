using System;
using System.Web.Http;
using HucaresServer.Storage.Helpers;

namespace HucaresServer.Controllers
{
    /// <summary>
    /// Responsible for managing the DetectedPlateController database table via http requests.
    /// </summary>
    /// <seealso cref="IMissingPlateHelper"/>
    public class MissingPlateController : ApiController
    {
        public IMissingPlateHelper MissingPlateHelper { get; set; } = new MissingPlateHelper();

        [HttpGet]
        [Route("api/mlp/all")]
        public IHttpActionResult GetAllMissingPlates()
        {
            return Json(MissingPlateHelper.GetAllPlateRecords());
        }

        [HttpGet]
        [Route("api/mlp/plate/{plateNumber}")]
        public IHttpActionResult GetAllMissingPlatesByPlateNumber(string plateNumber)
        {
            return Json(MissingPlateHelper.GetPlateRecordByPlateNumber(plateNumber));
        }

        [HttpGet]
        [Route("api/mlp/insert/{plateNumber}")]
        public IHttpActionResult InsertMissingPlateByNumber(string plateNumber, DateTime startDateTime)
        {
            return Json(MissingPlateHelper.InsertPlateRecord(plateNumber, startDateTime));
        }
        
        [HttpGet]
        [Route("api/mlp/update/{plateId}")]
        public IHttpActionResult UpdatePlateRecordById(int plateId, DateTime startDateTime)
        {
            return Json(MissingPlateHelper.UpdatePlateRecord(plateId, "5555", startDateTime));
        }
        
        [HttpGet]
        [Route("api/mlp/markfound/{plateId}")]
        public IHttpActionResult MarkFoundMissingPlate(int plateId, DateTime startDateTime)
        {
            return Json(MissingPlateHelper.MarkFoundPlate(plateId, startDateTime));
        }
        
        [HttpGet]
        [Route("api/mlp/marknotfound/{plateId}")]
        public IHttpActionResult MarkNotFoundMissingPlate(int plateId, DateTime startDateTime)
        {
            return Json(MissingPlateHelper.MarkNotFoundPlate(plateId, startDateTime));
        }
        
        [HttpGet]
        [Route("api/mlp/delete/{plateId}")]
        public IHttpActionResult DeletePlateRecordById(int plateId)
        {
            return Json(MissingPlateHelper.DeletePlateById(plateId));
        }
        
        [HttpGet]
        [Route("api/mlp/delete/{plateNumber}")]
        public IHttpActionResult DeletePlateRecordByNumber(string plateNumber)
        {
            return Json(MissingPlateHelper.DeletePlateByNumber(plateNumber));
        }
    }
}