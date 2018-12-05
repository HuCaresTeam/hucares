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
using HucaresServer.TimedProccess;
using System.Linq;
using HucaresServer.Properties;

namespace HucaresServer.Controllers
{
    public class ImageController : ApiController
    {
        public IImageManipulator ImageManipulator { get; set; } = new LocalImageManipulator();
        public IDetectedPlateHelper DetectedPlateHelper { get; set; } = new DetectedPlateHelper();
        public IMissingPlateHelper MissingPlateHelper { get; set; } = new MissingPlateHelper();
        public ILocationToUrlConverter LocationToUrl { get; set; } = new LocationToUrlConverter();

        //TODO: Make more testable
        [HttpGet]
        [Route("api/images/{dateTime}/{fileName}")]
        public IHttpActionResult GetImage(DateTime dateTime, string fileName)
        {

            var endpoint = LocationToUrl.ConvertPathToUrl(fileName, dateTime);
            var missingList = DetectedPlateHelper.GetDetectedPlatesByImgUrl(endpoint)
                .Select(dlp => MissingPlateHelper.GetPlateRecordByPlateNumber(dlp.PlateNumber))
                .Where(mlpList => 0 != mlpList.Count())
                .Where(mlpList => mlpList.Where(mlp => mlp.Status == LicensePlateFoundStatus.Searching).Any())
                .ToList();
            if (!missingList.Any())
            {
                throw new AccessViolationException(Resources.Error_FileAccessDenied);
            }

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
