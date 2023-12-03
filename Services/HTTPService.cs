using DNSUpdater.Config;
using DNSUpdater.Utils.Exceptions;
using Polly;
using System.Net;
using System.Text;

namespace DNSUpdater.Services
{
    public class HttpService
    {
        private readonly HttpClient _client;

        public HttpService()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All
            };

            _client = new HttpClient();
        }

        public async Task<string> GetAsync(string uri, int retry, int delay)
        {
            /*using HttpResponseMessage response = await _client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new ProjectException(DictionaryError.ERROR_UNABLE_TO_REACH_THE_REQUESTED_URL(uri));

            return await response.Content.ReadAsStringAsync();*/
            return await Policy
            .Handle<ProjectException>()
            //.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            //.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(retry, retryAttempt => TimeSpan.FromMilliseconds(delay))
            .ExecuteAsync(async () =>
            {
                try
                {
                    HttpResponseMessage response = await _client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    if (!response.IsSuccessStatusCode)
                        throw new ProjectException(DictionaryError.ERROR_UNABLE_TO_REACH_THE_REQUESTED_URL(uri));
                    return await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    throw new ProjectException("Exeption");
                }



            }
            ).ConfigureAwait(false);
            /*


            RetryPolicy <HttpResponseMessage> httpRetryPolicy = Policy
    .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
    .Or<HttpRequestException>()
    .WaitAndRetryAsync(3, retryAttempt =>
            TimeSpan.FromSeconds(Math.Pow(2, retryAttempt) / 2));

            HttpResponseMessage httpResponseMessage = await
            httpRetryPolicy.ExecuteAsync(
                () => httpClient.GetAsync(remoteEndpoint));*/


        }


        public async Task<string> PostAsync(string uri, string data, string contentType)
        {
            using HttpContent content = new StringContent(data, Encoding.UTF8, contentType);

            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                Content = content,
                Method = HttpMethod.Post,
                RequestUri = new Uri(uri)
            };

            using HttpResponseMessage response = await _client.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
                throw new ProjectException(DictionaryError.ERROR_UNABLE_TO_REACH_THE_REQUESTED_URL(uri));

            return await response.Content.ReadAsStringAsync();
        }
    }
}
