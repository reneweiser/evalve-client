using Evalve.Client;
using UnityEngine;
using Transform = Evalve.Client.Transform;

namespace Evalve
{
    public class SceneObjectSpawner : MonoBehaviour
    {
        [SerializeField] private SceneObjectFactory _sceneObjectFactory;

        public SceneObjectBehaviour SpawnAt(Vector3 position)
        {
            return _sceneObjectFactory.Create(new SceneObject
            {
                Transform = new Transform
                {
                    Position = position.ToVector(),
                    Rotation = Vector3.zero.ToVector(),
                },
                Properties = new Property[]
                {
                    new Body
                    {
                        Position = Vector3.up.ToVector(),
                        Rotation = Vector3.zero.ToVector(),
                    }
                }
            });
        }
    }
}