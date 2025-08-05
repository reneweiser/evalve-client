using System.Collections.Generic;
using Evalve.Panels.Elements;
using Evalve.States;
using UnityEngine;
using Pose = Evalve.SceneObjects.Pose;

namespace Evalve.Panels
{
    public class ListPoses : UiPanel
    {
        [SerializeField] private PoseElement _prefab;
        [SerializeField] private Transform _container;

        private readonly List<PosePresenter> _poses = new();
        
        public void AddPose(Pose pose)
        {
            var element = Instantiate(_prefab, _container);
            _poses.Add(new PosePresenter(pose, element));
        }

        public void Clear()
        {
            for (var i = _poses.Count - 1; i >= 0; i--)
            {
                _poses[i].ClearElement();
                _poses.RemoveAt(i);
            }
        }
    }
}