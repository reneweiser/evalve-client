using Evalve.Client;
using Evalve.Systems;
using UnityEngine;
using Transform = Evalve.Client.Transform;

namespace Evalve.SceneObjects
{
    public class Spawner : MonoBehaviour
    {
        public SceneObject SpawnAt(Vector3 position)
        {
            return Services.Get<Factory>().Create(new Client.SceneObject
            {
                Transform = new Transform
                {
                    Position = position.ToVector(),
                    Rotation = Vector3.zero.ToVector(),
                },
                Properties = new Property[] { }
            });
        }
    }
}