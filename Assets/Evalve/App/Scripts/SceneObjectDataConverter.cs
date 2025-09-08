using System;
using System.Linq;
using Evalve.Client;
using UnityEngine;
using Object = UnityEngine.Object;
using Pose = Evalve.Client.Pose;
using Transform = Evalve.Client.Transform;

namespace Evalve.App
{
    public static class SceneObjectDataConverter
    {
        public static SceneObject ToData(this SceneObjectBehaviour behaviour, SceneObject old)
        {
            old.Name = behaviour.name;
            old.ImageUrl = behaviour.ScreenshotPath;
            
            old.Transform = new Transform()
            {
                Position = behaviour.transform.position.ToVector(),
                Rotation = behaviour.transform.rotation.eulerAngles.ToVector(),
            };

            old.Properties = behaviour.GetComponentsInChildren<SceneObjectPose>()
                .Select(sceneObjectPose => (Property)(new Pose()
                {
                    Role = sceneObjectPose.role,
                    Position = sceneObjectPose.transform.position.ToVector(),
                    Rotation = sceneObjectPose.transform.rotation.eulerAngles.ToVector(),
                }))
                .ToArray();
            
            old.IsDirty = true;
        
            return old;
        }

        public static GameObject ToBehaviour(this SceneObject dto, PrefabRegistry registry)
        {
            var prefabId = dto.GetType().Name;
            var prefab = registry.GetPrefab(prefabId);

            if (prefab == null)
            {
                Debug.LogWarning($"Prefab with ID '{prefabId}' not found. Creating empty GameObject.");
                return new GameObject(prefabId);
            }

            var go = Object.Instantiate(prefab);
            go.GetComponent<SceneObjectBehaviour>().SetId(dto.Id);
            go.transform.SetPositionAndRotation(dto.Transform.Position.ToVector3(), Quaternion.Euler(dto.Transform.Rotation.ToVector3()));
            go.name = dto.Name;

            foreach (var childDto in dto.Properties)
            {
                var child = childDto.ToProperty(registry);
                child.transform.SetParent(go.transform, true);
            }

            return go;
        }

        public static GameObject ToProperty(this Property property, PrefabRegistry registry)
        {
            return property switch
            {
                Pose pose => CreatePose(registry, pose),
                Checkpoint checkpoint => CreateCheckpoint(registry, checkpoint),
                _ => throw new Exception()
            };
        }

        private static GameObject CreateCheckpoint(PrefabRegistry registry, Checkpoint checkpoint)
        {
            return Object.Instantiate(registry.GetPrefab(checkpoint.GetType().Name));
        }

        private static GameObject CreatePose(PrefabRegistry registry, Pose pose)
        {
            var p = Object.Instantiate(registry.GetPrefab(pose.GetType().Name)).GetComponent<SceneObjectPose>();
            p.name = pose.Id;
            p.role = pose.Role;
            p.id = pose.Id;
            p.objectId = pose.ObjectId;
            p.transform.SetPositionAndRotation(
                pose.Position.ToVector3(),
                Quaternion.Euler(pose.Rotation.ToVector3()));
            
            return p.gameObject;
        }
    }
}