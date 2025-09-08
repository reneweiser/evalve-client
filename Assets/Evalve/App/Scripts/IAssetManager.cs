using System.Collections.Generic;
using UnityEngine;

namespace Evalve.App
{
    public interface IAssetManager
    {
        void Register(string assetName, GameObject asset);
        void Unregister(string assetName);
        void SelectAssets(List<string> assetName);
        Dictionary<string,GameObject> GetAssets();
        void Cleanup();
    }
}