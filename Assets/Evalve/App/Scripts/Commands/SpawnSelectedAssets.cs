using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Evalve.Contracts;
using UnityEngine;
using ILogger = Evalve.Contracts.ILogger;
using LogType = Evalve.Contracts.LogType;

namespace Evalve.App.Commands
{
    public class SpawnSelectedAssets : ICommand
    {
        private readonly Session _session;
        private readonly ILogger _logger;
        private readonly AssetBundleLoader _assetBundleLoader;

        public SpawnSelectedAssets(Session session, ILogger logger, AssetBundleLoader assetBundleLoader)
        {
            _session = session;
            _logger = logger;
            _assetBundleLoader = assetBundleLoader;
        }
        public async Task Execute()
        {
            if (_session.UserSelectedAssets == null)
                return;
            
            _logger.Log("Spawning selected assets", LogType.Message);
            
            await Task.WhenAll(_session.UserAssets
                .Where(assetBundle => _session.UserSelectedAssets.Contains(assetBundle.Key))
                .Select(assetBundle =>
                {
                    var bundlePath = Path.Combine(Application.streamingAssetsPath, assetBundle.Key);
                    var assetNames = assetBundle.Value.Elements.Select(element => element.Name).ToArray();
                    
                    _logger.Log($"Loading asset: {assetBundle.Value.Name}", LogType.Message);
                    return _assetBundleLoader.LoadAssetsAsync(bundlePath, assetNames);
                }));
        }
    }
}