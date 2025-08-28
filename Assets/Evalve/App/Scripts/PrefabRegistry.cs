using System.Collections.Generic;
using UnityEngine;

namespace Evalve.App
{
    [CreateAssetMenu(fileName = "PrefabRegistry", menuName = "Evalve/Prefab Registry")]
    public class PrefabRegistry : ScriptableObject
    {
        public List<PrefabEntry> prefabs;

        private Dictionary<string, GameObject> _lookup;

        public GameObject GetPrefab(string id)
        {
            if (_lookup == null)
            {
                _lookup = new Dictionary<string, GameObject>();
                foreach (var entry in prefabs)
                    _lookup[entry.id] = entry.prefab;
            }
            return _lookup.GetValueOrDefault(id);
        }
    }
}