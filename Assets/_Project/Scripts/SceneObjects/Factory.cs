using System;
using Evalve.Client;
using UnityEngine;
using ClientTransform = Evalve.Client.Transform;

namespace Evalve.SceneObjects
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] private SceneObject _sceneObjectPrefab;
        [SerializeField] private BimData _bimDataPrefab;
        [SerializeField] private Body _bodyPrefab;
        [SerializeField] private Checkpoint _checkpointPrefab;
        [SerializeField] private Pose _posePrefab;

        public SceneObject CreateTemporary(Vector3 position)
        {
            return Create(new Client.SceneObject
            {
                Id = Ulid.NewUlid().ToString(),
                TeamId = "01k1atqfmqzms79n6erv1k4dq2",
                Name = "New Scene Object",
                Transform = new ClientTransform
                {
                    Position = position.ToVector(),
                    Rotation = Vector3.zero.ToVector(),
                },
                Properties = new Property[] { }
            });
        }

        public SceneObject CreateNewAt(Vector3 position)
        {
            return Create(new Client.SceneObject
            {
                Id = Ulid.NewUlid().ToString(),
                TeamId = "01k1atqfmqzms79n6erv1k4dq2",
                Name = "New Scene Object",
                Transform = new ClientTransform
                {
                    Position = position.ToVector(),
                    Rotation = Vector3.zero.ToVector(),
                },
                Properties = new Property[] { }
            });
        }
        
        public SceneObject CreateFromData(Client.SceneObject data)
        {
            return Create(data);
        }

        private SceneObject Create(Client.SceneObject data)
        {
            var obj = Instantiate(
                _sceneObjectPrefab,
                data.Transform.Position.ToVector3(),
                Quaternion.Euler(data.Transform.Rotation.ToVector3()));

            obj.name = $"sceneObject_{data.Name}";
            obj.SetData(data);
            
            foreach (var property in data.Properties)
            {
                MonoBehaviour res = property switch
                {
                    Client.BimData bimData => CreateBimData(bimData, obj),
                    Client.Body body => CreateBody(body, obj),
                    Client.Checkpoint checkpoint => CreateCheckpoint(checkpoint, obj),
                    Client.Pose pose => CreatePose(pose, obj),
                    _ => throw new InvalidOperationException("Unknown property type: " + property.GetType().FullName)
                };
            }

            return obj;
        }

        private Pose CreatePose(Client.Pose data, SceneObject sceneObject)
        {
            var obj = Instantiate(_posePrefab, sceneObject.transform);
            obj.name = data.Role;
            obj.transform.SetPositionAndRotation(data.Position.ToVector3(),
                Quaternion.Euler(data.Rotation.ToVector3()));

            return obj;
        }

        private Checkpoint CreateCheckpoint(Client.Checkpoint data, SceneObject sceneObject)
        {
            var obj = Instantiate(_checkpointPrefab, sceneObject.transform);
            obj.transform.localPosition = new Vector3(0f, 0.1f, 0f);
            obj.SetPerimeter(data.Perimeter);

            return obj;
        }

        private Body CreateBody(Client.Body data, SceneObject sceneObject)
        {
            var obj = Instantiate(_bodyPrefab, sceneObject.transform);
            obj.transform.localPosition = data.Position.ToVector3();
            obj.transform.localEulerAngles = data.Rotation.ToVector3();

            return obj;
        }

        private BimData CreateBimData(Client.BimData data, SceneObject sceneObject)
        {
            return Instantiate(_bimDataPrefab,
                data.SurveyPointPosition.ToVector3(),
                Quaternion.identity,
                sceneObject.transform);
        }
    }
}