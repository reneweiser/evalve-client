using System.Linq;
using Evalve.Client;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using SceneObject = Evalve.SceneObjects.SceneObject;
using Transform = Evalve.Client.Transform;

namespace Evalve.Systems
{
    public abstract class SceneObjectProperty : MonoBehaviour
    {
        public abstract Property Property { get; }
    }
    public class SceneObjectSerializer
    {
        public Client.SceneObject Serialize(SceneObject sceneObject)
        {
            var properties = sceneObject.GetComponentsInChildren<SceneObjectProperty>()
                .Select(property => property.Property)
                .ToArray();

            return new Client.SceneObject()
            {
                Id = sceneObject.Data.Id,
                Name = sceneObject.Data.Name,
                TeamId = sceneObject.Data.TeamId,
                Transform = new Transform
                {
                    Position = sceneObject.transform.position.ToVector(),
                    Rotation = sceneObject.transform.eulerAngles.ToVector(),
                },
                Properties = properties,
            };
        }
    }
}