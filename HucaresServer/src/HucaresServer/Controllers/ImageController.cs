using HucaresServer.Storage.Models;
using System;
using System.IO;
using System.Web.Http;
using HucaresServer.Storage.Helpers;
using HucaresServer.DataAcquisition;
using HucaresServer.TimedProccess;
using System.Linq;
using HucaresServer.Properties;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

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

            var filePath = Path.Combine(folderLocation, fileName) + ".jpg";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(File.OpenRead(filePath)),
                StatusCode = HttpStatusCode.OK
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            return ResponseMessage(response);
        }
    }
}
