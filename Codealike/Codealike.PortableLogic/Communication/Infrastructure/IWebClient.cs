using System;
using System.Net.Http;

namespace Codealike.PortableLogic.Communication.Infrastructure
{
    using System.Threading.Tasks;

    public interface IWebClient
    {
        Task<WebApiCallReport> GetAsync(HttpRequestMessage request);

        void AddHeader(string name, string value);
    }
}
