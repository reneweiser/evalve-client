using System;
using Evalve.Api;
using UnityEngine;
using Pose = Evalve.Api.Pose;

namespace Evalve
{
    public class SceneObjectFactory : MonoBehaviour
    {
        [SerializeField] SceneObjectBehaviour _sceneObjectPrefab;
        [SerializeField] BimDataBehaviour _bimDataPrefab;
        [SerializeField] BodyBehaviour _bodyPrefab;
        [SerializeField] CheckpointBehaviour _checkpointPrefab;
        [SerializeField] PoseBehaviour _posePrefab;
        
        public SceneObjectBehaviour Create(SceneObject data)
        {
            var obj = Instantiate(
                _sceneObjectPrefab,
                data.Transform.Position.ToVector3(),
                Quaternion.Euler(data.Transform.Rotation.ToVector3()));
            
            foreach (var property in data.Properties)
            {
                MonoBehaviour res = property switch
                {
                    BimData bimData => CreateBimData(bimData, obj),
                    Body body => CreateBody(body, obj),
                    Checkpoint checkpoint => CreateCheckpoint(checkpoint, obj),
                    Pose pose => CreatePose(pose, obj),
                    _ => throw new InvalidOperationException("Unknown property type")
                };
            }
            
            return obj;
        }

        private PoseBehaviour CreatePose(Pose data, SceneObjectBehaviour sceneObjectBehaviour)
        {
            var obj = Instantiate(_posePrefab, sceneObjectBehaviour.transform);
            obj.name = data.Role;
            obj.transform.SetPositionAndRotation(data.Position.ToVector3(),
                Quaternion.Euler(data.Rotation.ToVector3()));

            return obj;
        }

        private CheckpointBehaviour CreateCheckpoint(Checkpoint data, SceneObjectBehaviour sceneObjectBehaviour)
        {
            var obj = Instantiate(_checkpointPrefab, sceneObjectBehaviour.transform);
            obj.transform.localPosition = new Vector3(0f, 0.1f, 0f);
            obj.SetPerimeter(data.Perimeter);

            return obj;
        }

        private BodyBehaviour CreateBody(Body data, SceneObjectBehaviour sceneObjectBehaviour)
        {
            var obj = Instantiate(_bodyPrefab, sceneObjectBehaviour.transform);
            obj.transform.localPosition = data.Position.ToVector3();
            obj.transform.localEulerAngles = data.Rotation.ToVector3();

            return obj;
        }

        private BimDataBehaviour CreateBimData(BimData data, SceneObjectBehaviour sceneObjectBehaviour)
        {
            return Instantiate(_bimDataPrefab,
                data.SurveyPointPosition.ToVector3(),
                Quaternion.identity,
                sceneObjectBehaviour.transform);
        }
    }
}