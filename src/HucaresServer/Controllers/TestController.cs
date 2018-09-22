using HucaresServer.Storage;
using HucaresServer.Storage.Models;
using Newtonsoft.Json;
using System;
using System.Web.Http;

namespace HucaresServer.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("api/test/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Json($"Hello world {id}");
        }

        [HttpPost]
        [Route("api/test")]
        public IHttpActionResult Post([FromBody] MLPPostParams mlpParams)
        {
            using (var ctx = new HucaresContext())
            {
                var mlp = new MissingLicensePlate()
                {
                    PlateNumber = mlpParams.PlateNumber,
                    SearchStartDateTime = mlpParams.SearchStartDateTime
                };

                ctx.MissingLicensePlates.Add(mlp);
                ctx.SaveChanges();

                var generatedJson = JsonConvert.SerializeObject(mlp);
                return Json(mlp);
            }
        }
    }

    public class MLPPostParams
    {
        public string PlateNumber { get; set; }
        public DateTime SearchStartDateTime { get; set; }
    }
}
