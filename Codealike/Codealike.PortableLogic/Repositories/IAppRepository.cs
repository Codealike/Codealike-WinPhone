using Codealike.PortableLogic.Communication.Services;

namespace Codealike.PortableLogic.Repositories
{
    public interface IAppRepository
    {
        void SaveCredentials(Credentials credentials);
        Credentials LoadCredentials();
        void DeleteCredentials();
    }
}