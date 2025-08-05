using System.Collections.Generic;
using UnityEngine;

namespace Evalve.Systems
{
    public class AssetManager : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private readonly Dictionary<GameObject, bool> _assets = new();

        public void RegisterAsset(GameObject asset)
        {
            asset.SetActive(false);
            _assets[asset] = false;
        }

        public void Toggle(GameObject asset)
        {
            if (!_assets.TryGetValue(asset, out var isActive))
            {
                Debug.LogWarning($"Asset {asset.name} has not been registered");
                return;
            }
            _assets[asset] = !_assets[asset];
            asset.SetActive(_assets[asset]);
        }
    }
}