using System.Collections.Generic;
using System.Linq;

namespace Evalve.App.States
{
    public class IdleModel : Model
    {
        private readonly IAssetManager _assetManager;
        private readonly IObjectManager _objectManager;

        public IdleModel(IAssetManager assetManager, IObjectManager objectManager)
        {
            _assetManager = assetManager;
            _objectManager = objectManager;
            AssetsActive = _assetManager.GetAssets()
                .ToDictionary(i => i.Value.name, i => i.Value.gameObject.activeInHierarchy);
        }
        
        public Dictionary<string,bool> AssetsActive { get; }
        
        public Dictionary<string,string> AssetOptions => _assetManager.GetAssets()
            .ToDictionary(i => i.Key, i => i.Key);
        
        public Dictionary<string,string> ObjectOptions => _objectManager.GetObjects()
            .ToDictionary(i => i.GetId(), i => i.name);
    }
}