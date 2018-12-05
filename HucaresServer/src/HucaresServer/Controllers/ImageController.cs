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
using System.Threading.Tasks;

namespace HucaresServer.Controllers
{
    public class ImageController : ApiController
    {
        public IImageManipulator ImageManipulator { get; set; } = new LocalImageManipulator();

        //TODO: Make more testable
        [HttpGet]
        [Route("api/images/{dateTime}/{fileName}")]
        public IHttpActionResult GetImage(DateTime dateTime, string fileName)
        {
            var folderLocation = ImageManipulator.GenerateFolderLocationPath(dateTime);

            //TODO: Add regex check for ../

            var fileLocation = Path.Combine(folderLocation, fileName) + ".jpg";

            using (Image image = Image.FromFile(fileLocation))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);

                    return Json(new GetImageResponse(base64String));
                }
            }

        }
    }

    public class GetImageResponse
    {
        public GetImageResponse(string imageBase64)
        {
            ImageBase64 = imageBase64;
        }

        public string ImageBase64 { get; set; }
    }
}
