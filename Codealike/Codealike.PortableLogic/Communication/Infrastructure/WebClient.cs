namespace Codealike.PortableLogic.Communication.Infrastructure
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class WebClient : IWebClient
    {
        private HttpClient _httpClient;

        public WebClient()
        {
            _httpClient = new HttpClient();            
        }

        public async Task<WebApiCallReport> GetAsync(HttpRequestMessage httpRequest)
        {            
            var responseMessage = await _httpClient.SendAsync(httpRequest);
            var webApiCallReport = new WebApiCallReport
            {
                HttpCode = responseMessage.StatusCode,
                StringResponse = await responseMessage.Content.ReadAsStringAsync(),
                ErrorMessage = string.Empty,
                Successful = responseMessage.IsSuccessStatusCode
            };
            if ( responseMessage.IsSuccessStatusCode == false )
                throw new WebApiException(webApiCallReport);

            return webApiCallReport;
        }

        public void AddHeader(string name, string value)
        {
            _httpClient.DefaultRequestHeaders.Add(name, value);
        }
    }
}