using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hucares.Server.Client.Models;
using Newtonsoft.Json;

namespace Hucares.Server.Client
{
    public class DetectedPlateClient : IDetectedPlateClient
    {
        public Uri HostUri { get; private set; } = new Uri("http://localhost:50510");
        private readonly HttpClientHelper httpHelper;

        public DetectedPlateClient(HttpClientHelper httpHelper = null)
        {
            this.httpHelper = httpHelper ?? new HttpClientHelper();
        }
        
        public async Task<IEnumerable<DetectedLicensePlate>> GetAllDetectedMissingPlates()
        {
            var uri = "api/dlp/all";
            var fullUri = new Uri(HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Get, fullUri);

            return await httpHelper.MakeRequest<IEnumerable<DetectedLicensePlate>>(request);
        }

        public async Task<IEnumerable<DetectedLicensePlate>> GetAllDetectedPlatesByPlateNumber(string plateNumber, 
            DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            var uri = $"api/dlp/plate/{plateNumber}";
            var fullUri = new Uri(HostUri, uri);

            if (startDateTime != null)
            {
                uri += $"?startDateTime={startDateTime}";

                if (endDateTime != null)
                {
                    uri += $"&endDateTime={endDateTime}";
                }
            } 
            else if (endDateTime != null)
            {
                uri += $"?endDateTime={endDateTime}";
            }

            var request = new HttpRequestMessage(HttpMethod.Get, fullUri);

            return await httpHelper.MakeRequest<IEnumerable<DetectedLicensePlate>>(request);
        }

        public async Task<IEnumerable<DetectedLicensePlate>> GetAllDetectedPlatesByCamera(int cameraId, 
            DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            var uri = $"api/dlp/cam/{cameraId}";
            var fullUri = new Uri(HostUri, uri);

            if (startDateTime != null)
            {
                uri += $"?startDateTime={startDateTime}";

                if (endDateTime != null)
                {
                    uri += $"&endDateTime={endDateTime}";
                }
            } 
            else if (endDateTime != null)
            {
                uri += $"?endDateTime={endDateTime}";
            }

            var request = new HttpRequestMessage(HttpMethod.Get, fullUri);

            return await httpHelper.MakeRequest<IEnumerable<DetectedLicensePlate>>(request);
        }
    }
}