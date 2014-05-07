using System.Net;

namespace Codealike.PortableLogic.Communication.Infrastructure
{
    public class WebReport
    {
        public HttpStatusCode HttpCode { get; set; }
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }
    }
}