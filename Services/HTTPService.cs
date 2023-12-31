﻿using DNSUpdater.Config;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Utils.Exceptions;
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

        public async Task<string> GetAsync(string uri)
        {
            using HttpResponseMessage response = await _client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
                throw new ProjectException(DictionaryError.ERROR_UNABLE_TO_REACH_THE_REQUESTED_URL(uri));

            return await response.Content.ReadAsStringAsync();
        }


        public async Task<string> SendAsync(string uri, string method, string data, List<PropertiesDTO> headers, string contentType)
        {
            HttpMethod methodHTTP = HttpMethod.Post;

            switch (true)
            {
                case bool b when method.Equals(BusinessConfig.HTTP_GET.ToLower(), StringComparison.InvariantCultureIgnoreCase):
                    methodHTTP = HttpMethod.Get;
                    break;
                case bool b when method.Equals(BusinessConfig.HTTP_POST.ToLower(), StringComparison.InvariantCultureIgnoreCase):
                    methodHTTP = HttpMethod.Post;
                    break;
                case bool b when method.Equals(BusinessConfig.HTTP_DELETE.ToLower(), StringComparison.InvariantCultureIgnoreCase):
                    methodHTTP = HttpMethod.Delete;
                    break;
                case bool b when method.Equals(BusinessConfig.HTTP_PATCH.ToLower(), StringComparison.InvariantCultureIgnoreCase):
                    methodHTTP = HttpMethod.Patch;
                    break;
                case bool b when method.Equals(BusinessConfig.HTTP_PUT.ToLower(), StringComparison.InvariantCultureIgnoreCase):
                    methodHTTP = HttpMethod.Put;
                    break;
                default: throw new ArgumentException(DictionaryError.ERROR_INVALID_HTTP_VERB_PROVIDED(method));
            }
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(uri);
            requestMessage.Method = methodHTTP;

            if (!methodHTTP.Equals(HttpMethod.Get))
            {
                using HttpContent content = new StringContent(data, Encoding.UTF8, contentType);
                requestMessage.Content = content;
            }

            headers.ForEach(element => requestMessage.Headers.Add(element.Name, element.Value));

            using HttpResponseMessage response = await _client.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
                throw new ProjectException(DictionaryError.ERROR_UNABLE_TO_REACH_THE_REQUESTED_URL(uri));

            return await response.Content.ReadAsStringAsync();
        }
    }
}
