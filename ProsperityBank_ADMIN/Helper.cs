using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ProsperityBank_ADMIN.Web.Helper
{
    public static class AdminApi
    {
        private const string ApiBaseUri = "http://localhost:40613";

        public static HttpClient InitializeClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ApiBaseUri) };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
