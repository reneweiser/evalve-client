using System.Collections.Generic;
using UnityEngine;

namespace Evalve.App
{
    public class AssetManager : IAssetManager
    {
        private readonly Dictionary<string, GameObject> _assets = new();
        
        public void Register(string assetName, GameObject asset)
        {
            _assets.Add(assetName, asset);
        }

        public void Unregister(string assetName)
        {
            _assets.Remove(assetName);
        }

        public void SelectAssets(List<string> assetNames)
        {
            foreach (var asset in _assets.Values)
            {
                asset.SetActive(assetNames.Contains(asset.name));
            }
        }

        public Dictionary<string, GameObject> GetAssets()
        {
            return _assets;
        }

        public void Cleanup()
        {
            foreach (var asset in _assets.Values)
            {
                Object.Destroy(asset);
            }
            
            _assets.Clear();
        }
    }
}