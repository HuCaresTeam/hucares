using Newtonsoft.Json;
using SqliteManipulation.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hucares.Server.Client
{
    public class CameraInfoClient : ICameraInfoClient
    {
        private readonly HttpClientHelper httpHelper;

        public CameraInfoClient(HttpClientHelper httpHelper = null)
        {
            this.httpHelper = httpHelper ?? new HttpClientHelper();
        }

        public async Task<Camera> InsertCamera(Camera camera)
        {
            var uri = "api/camera/insert";
            var fullUri = new Uri(httpHelper.HostUri, uri);

            var jsonContent = JsonConvert.SerializeObject(camera, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var request = new HttpRequestMessage(HttpMethod.Post, fullUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            return await httpHelper.MakeRequest<Camera>(request);
        }

        public async Task DeleteAllCameras(bool? isTrustedSource = null)
        {
            var uri = "api/camera/all";
            if (null != isTrustedSource)
            {
                uri += $"?isTrustedSource={isTrustedSource}";
            }

            var fullUri = new Uri(httpHelper.HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Delete, fullUri);

            await httpHelper.MakeRequest(request);
        }
    }
}
