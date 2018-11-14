using HucaresServer.Utils;
using OpenAlprApi.Api;
using OpenAlprApi.Model;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace HucaresServer.DataAcquisition
{
    public class OpenAlprWrapper : IOpenAlprWrapper
    {
        //TODO: Get values from config
        #region Configurables
        /// <summary>
        /// The secret key used to authenticate your account.  
        /// You can view your  secret key by visiting  https://cloud.openalpr.com/ 
        /// </summary>
        private readonly string secretKey = "INSERT_SECRET_KEY_HERE";

        /// <summary>
        /// Defines the training data used by OpenALPR.  
        /// \"us\" analyzes  North-American style plates.  
        /// \"eu\" analyzes European-style plates.  
        /// This field is required if using the \"plate\" task
        /// You may use multiple datasets by using commas between the country  codes.
        /// For example, 'au,auwide' would analyze using both the  Australian plate styles.
        /// A full list of supported country codes  can be found here https://github.com/openalpr/openalpr/tree/master/runtime_data/config 
        /// </summary>
        private readonly string country = "EU";

        /// <summary>
        /// If set to 1, the vehicle will also be recognized in the image 
        /// This requires an additional credit per request  (optional)  (default to 0)
        /// </summary>
        private readonly int? recognizeVehicle = 0;

        /// <summary>
        /// Corresponds to a US state or EU country code used by OpenALPR pattern recognition.  
        /// For example, using \"md\" matches US plates against the  Maryland plate patterns.  
        /// Using \"fr\" matches European plates against the French plate patterns.
        /// </summary>
        private readonly string state = "LT";

        /// <summary>
        /// If set to 1, the image you uploaded will be encoded in base64 and  sent back along with the response.
        /// </summary>
        private readonly int? returnImage = 0;

        /// <summary>
        /// The number of results you would like to be returned for plate  candidates and vehicle classifications  
        /// optional, default to 10
        /// </summary>
        private readonly int? topn = 5;

        /// <summary>
        /// Prewarp configuration is used to calibrate the analyses for the  angle of a particular camera.
        /// More information is available here http://doc.openalpr.com/accuracy_improvements.html#calibration  
        /// (optional)  (default to String.Empty)
        /// </summary>
        private readonly string prewarp = String.Empty;
        #endregion

        private readonly IDefaultApi _defaultApi;
        private readonly IMemoryStreamFactory _streamFactory;
        private readonly IImageWrapper _imageWrapper;

        public OpenAlprWrapper(IDefaultApi defaultApi = null, IMemoryStreamFactory streamFactory = null, IImageWrapper imageWrapper = null)
        {
            _defaultApi = defaultApi ?? new DefaultApi();
            _streamFactory = streamFactory ?? new MemoryStreamFactory();
            _imageWrapper = imageWrapper ?? new ImageWrapper();
        }

        public async Task<InlineResponse200> DetectPlateAsync(string pathToPlateImage)
        {
            if (string.IsNullOrEmpty(pathToPlateImage))
            {
                throw new ArgumentException("Argument should not be null or empty");
            }

            using (Image image = _imageWrapper.GetImageFromFile(pathToPlateImage))
            {
                using (MemoryStream m = _streamFactory.Create())
                {
                    image.Save(m, image.RawFormat);
                    var imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);

                    return await _defaultApi.RecognizeBytesAsync(base64String, secretKey, country, recognizeVehicle, state, returnImage, topn, prewarp);
                }
            }
        }
    }
}
