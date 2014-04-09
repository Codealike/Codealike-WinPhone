namespace Codealike.PortableLogic.Communication.Services
{
    using System.Threading.Tasks;

    using ApiModels;
    using Infrastructure;
    
    public interface IUserDataService
    {
        Task<WebApiCallReport<UserData>> GetUserData(Credentials credentials);
    }
}
