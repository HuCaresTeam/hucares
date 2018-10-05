using System;
using System.Web.Http;
using HucaresServer.Models;
using HucaresServer.Storage.Helpers;
using static HucaresServer.Models.MissingLicensePlateDataModels;

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

        [HttpPost]
        [Route("api/mlp/insert/{plateNumber}")]
        public IHttpActionResult InsertMissingPlateByNumber([FromBody] PostPlateRecordDataModel data)
        {
            return Json(MissingPlateHelper.InsertPlateRecord(data.PlateNumber, data.SearchStartDateTime));
        }
        
        [HttpPost]
        [Route("api/mlp/update/{plateId}")]
        public IHttpActionResult UpdatePlateRecordById(int id, [FromBody] PostPlateRecordDataModel data)
        {
            return Json(MissingPlateHelper.UpdatePlateRecord(id, data.PlateNumber, data.SearchStartDateTime));
        }
        
        [HttpPost]
        [Route("api/mlp/found/{plateId}/{isFound}")]
        public IHttpActionResult MarkFoundMissingPlate(int plateId, [FromBody] MarkFoundRecordDataModel data)
        {
            return Json(MissingPlateHelper.MarkFoundPlate(plateId, data.EndDateTime, data.IsFound));
        }
        
        [HttpDelete]
        [Route("api/mlp/delete/{plateId}")]
        public IHttpActionResult DeletePlateRecordById(int plateId)
        {
            return Json(MissingPlateHelper.DeletePlateById(plateId));
        }
        
        [HttpDelete]
        [Route("api/mlp/delete/{plateNumber}")]
        public IHttpActionResult DeletePlateRecordByNumber(string plateNumber)
        {
            return Json(MissingPlateHelper.DeletePlateByNumber(plateNumber));
        }
    }
}