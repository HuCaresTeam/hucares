using Hucares.Server.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hucares.Server.Client
{
    public class CameraInfoClient : ICameraInfoClient
    {
        public Uri HostUri { get; private set; } = new Uri("http://localhost:50510");
        private readonly HttpClientHelper httpHelper;

        public CameraInfoClient(HttpClientHelper httpHelper = null)
        {
            this.httpHelper = httpHelper ?? new HttpClientHelper();
        }

        public async Task<CameraInfo> InsertCamera(CameraInfo cameraInfo)
        {
            var uri = "api/camera/insert";
            var fullUri = new Uri(HostUri, uri);

            var jsonContent = JsonConvert.SerializeObject(cameraInfo, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var request = new HttpRequestMessage(HttpMethod.Post, fullUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            return await httpHelper.MakeRequest<CameraInfo>(request);
        }

        public async Task<CameraInfo> UpdateCameraSource(CameraInfo cameraInfo)
        {
            var uri = $"api/camera/update/source/{cameraInfo.Id}";
            var fullUri = new Uri(HostUri, uri);

            var jsonContent = JsonConvert.SerializeObject(cameraInfo, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var request = new HttpRequestMessage(HttpMethod.Post, fullUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            return await httpHelper.MakeRequest<CameraInfo>(request);
        }

        public async Task<CameraInfo> UpdateCameraActivity(CameraInfo cameraInfo)
        {
            var uri = $"api/camera/update/activity/{cameraInfo.Id}";
            var fullUri = new Uri(HostUri, uri);

            var jsonContent = JsonConvert.SerializeObject(cameraInfo, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var request = new HttpRequestMessage(HttpMethod.Post, fullUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            return await httpHelper.MakeRequest<CameraInfo>(request);
        }

        public async Task<IEnumerable<CameraInfo>> GetAllCameras(bool? isTrustedSource = null)
        {
            var uri = "api/camera/all";
            if (null != isTrustedSource)
            {
                uri += $"?isTrustedSource={isTrustedSource}";
            }

            var fullUri = new Uri(HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Get, fullUri);
            return await httpHelper.MakeRequest<IEnumerable<CameraInfo>>(request);
        }

        public async Task<IEnumerable<CameraInfo>> GetActiveCameras(bool? isTrustedSource = null)
        {
            var uri = "api/camera/active";
            if (null != isTrustedSource)
            {
                uri += $"?isTrustedSource={isTrustedSource}";
            }

            var fullUri = new Uri(HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Get, fullUri);
            return await httpHelper.MakeRequest<IEnumerable<CameraInfo>>(request);
        }

        public async Task<IEnumerable<CameraInfo>> GetInactiveCameras()
        {
            var uri = "api/camera/inactive";
            var fullUri = new Uri(HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Get, fullUri);
            return await httpHelper.MakeRequest<IEnumerable<CameraInfo>>(request);
        }
    }
}
