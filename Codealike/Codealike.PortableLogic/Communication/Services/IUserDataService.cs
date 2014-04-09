using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Codealike.PortableLogic.Communication.ApiModels;
using Codealike.PortableLogic.Communication.Infrastructure;
using Newtonsoft.Json;

namespace Codealike.PortableLogic.Communication.Services
{
    public interface IUserDataService
    {
        Task<WebApiCallReport<UserData>> GetUserData(Credentials credentials);
    }

    public class UserDataService : IUserDataService
    {
        private readonly IWebClient _webClient;

        public UserDataService(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<WebApiCallReport<UserData>> GetUserData(Credentials credentials)
        {
            WebApiCallReport<UserData> report = new WebApiCallReport<UserData>();
            try
            {
                Uri uri = new Uri("https://codealike.com/api/v2/facts/" + credentials.Username);
                var request = new HttpRequestMessage()
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
                    if (webException.WebApiCallReport.HttpCode == HttpStatusCode.Forbidden)
                    {
                        report.ErrorMessage = "The credentials are invalid";
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
