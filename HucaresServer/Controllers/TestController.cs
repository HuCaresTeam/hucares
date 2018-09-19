using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
