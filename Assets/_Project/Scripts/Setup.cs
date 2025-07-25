using System;
using Evalve.Api;
using UnityEngine;

namespace Evalve
{
    public class Setup : MonoBehaviour
    {
        [SerializeField] private SceneObjectFactory _sceneObjectFactory;
        [SerializeField] private SceneObjectBehaviour _prefab;

        private async void Start()
        {
            const string url = "http://localhost";

            var connection = new Connection(url);
            
            var sceneObjects = await connection.GetSceneObjectsAsync();

            foreach (var sceneObject in sceneObjects)
            {
                _sceneObjectFactory.Create(sceneObject);
            }
        }
    }
}
