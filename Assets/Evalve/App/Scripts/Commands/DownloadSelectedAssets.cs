using System.Linq;
using System.Threading.Tasks;
using Evalve.Client;
using Evalve.Contracts;

namespace Evalve.App.Commands
{
    public class DownloadSelectedAssets : ICommand
    {
        private readonly Session _session;
        private readonly IConnection _connection;

        public DownloadSelectedAssets(Session session, IConnection connection)
        {
            _session = session;
            _connection = connection;
        }
        
        public async Task Execute()
        {
            if (_session.UserSelectedAssets == null)
                return;
            
            await Task.WhenAll(_session.UserAssets
                .Where(assetBundle => _session.UserSelectedAssets.Contains(assetBundle.Key))
                .Select(assetBundle => _connection.DownloadAssetBundleFileAsync(assetBundle.Value)));
        }
    }
}