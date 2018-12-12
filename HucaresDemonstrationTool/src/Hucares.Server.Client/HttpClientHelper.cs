using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hucares.Server.Client
{
    public class HttpClientHelper
    {
        private readonly static HttpClient httpClient = new HttpClient();

        public async Task<TOut> MakeRequest<TOut>(HttpRequestMessage requestMessage)
        {
            var response = (await httpClient.SendAsync(requestMessage))
                .EnsureSuccessStatusCode();
            if (response.Content == null)
            {
                return default(TOut);
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<TOut>(responseContent);

            return responseObject;
        }

        public async Task<string> MakeRequest(HttpRequestMessage requestMessage)
        {
            var response = (await httpClient.SendAsync(requestMessage))
                .EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
