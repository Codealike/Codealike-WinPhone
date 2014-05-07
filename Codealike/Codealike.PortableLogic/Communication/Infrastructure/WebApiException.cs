namespace Codealike.PortableLogic.Communication.Infrastructure
{
    using System;

    public class WebApiException : Exception
    {
        public WebApiCallReport WebApiCallReport { get; set; }

        public WebApiException(WebApiCallReport webApiCallReport)
        {
            WebApiCallReport = webApiCallReport;
        }
    }
}