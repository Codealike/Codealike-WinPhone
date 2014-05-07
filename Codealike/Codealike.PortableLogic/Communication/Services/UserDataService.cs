namespace Codealike.PortableLogic.Communication.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using ApiModels;
    using Infrastructure;
    
    public class UserDataService : IUserDataService
    {
        private readonly IWebClient _webClient;

        public UserDataService(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<WebApiCallReport<UserData>> GetUserData(Credentials credentials)
        {
            var report = new WebApiCallReport<UserData>();
            try
            {
                Uri uri = new Uri("https://codealike.com/api/v2/facts/" + credentials.Username);
                var request = new HttpRequestMessage
                {
                    RequestUri = uri,
                    Method = HttpMethod.Get,
                };
                request.Headers.Add("X-Api-Identity", credentials.Identity);
                request.Headers.Add("X-Api-Token", credentials.Token);
                var webApiCallReport = await _webClient.GetAsync(request);
                if (webApiCallReport.Successful)
                {
                    report.Successful = true;
                    report.Content = JsonConvert.DeserializeObject<UserData>(webApiCallReport.StringResponse);
                    return report;
                }
            }
            catch (Exception exception)
            {
                if (exception is WebApiException)
                {
                    var webException = exception as WebApiException;
                    if (webException.WebApiCallReport.HttpCode == HttpStatusCode.Forbidden || webException.WebApiCallReport.HttpCode == HttpStatusCode.Unauthorized)
                    {
                        report.ErrorMessage = "The user or token is not valid or you haven't made the Codealike data public";
                        return report;
                    }
                    if (webException.WebApiCallReport.HttpCode == HttpStatusCode.NotFound)
                    {
                        report.ErrorMessage = "Please check your internet connection";
                        return report;
                    }
                }
                report.ErrorMessage = "An unknown error occurred! Please try again!";
                return report;
            }
            return report;

        }
    }
}