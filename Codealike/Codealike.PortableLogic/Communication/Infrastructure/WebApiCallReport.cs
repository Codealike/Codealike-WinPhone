namespace Codealike.PortableLogic.Communication.Infrastructure
{
    public class WebApiCallReport: WebReport
    {
        public string StringResponse { get; set; }
    }

    public class WebApiCallReport<T>: WebReport
    {
        public T Content { get; set; }
    }
}
