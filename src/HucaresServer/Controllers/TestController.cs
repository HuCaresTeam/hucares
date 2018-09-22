using HucaresServer.Storage;
using HucaresServer.Storage.Models;
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
            using (var ctx = new HucaresContext())
            {
                var mlp = new MissingLicensePlate()
                {
                    PlateNumber = mlpParams.PlateNumber,
                    SearchStartDateTime = mlpParams.SearchStartDateTime
                };

                ctx.MissingLicensePlates.Add(mlp);
                ctx.SaveChanges();

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
