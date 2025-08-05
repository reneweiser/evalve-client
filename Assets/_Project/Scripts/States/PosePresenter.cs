using Evalve.Panels;
using Evalve.Panels.Elements;
using Evalve.Systems;
using UnityEngine;
using Pose = Evalve.SceneObjects.Pose;

namespace Evalve.States
{
    public class PosePresenter
    {
        private readonly Pose _pose;
        private readonly PoseElement _poseElement;
        private readonly Avatar _avatar;

        public PosePresenter(Pose pose, PoseElement poseElement)
        {
            _pose = pose;
            _poseElement = poseElement;
            _avatar = Services.Get<Avatar>();

            _poseElement.ShowSelected += Show;
            _poseElement.DeleteSelected += Delete;
            _poseElement.Refresh(pose);
        }

        public void Show()
        {
            _avatar.Teleport(_pose.transform.position, _pose.transform.eulerAngles.y);
        }

        public void Delete()
        {
            Object.Destroy(_pose.gameObject);
            Object.Destroy(_poseElement.gameObject);
        }

        public void ClearElement()
        {
            Object.Destroy(_poseElement.gameObject);
        }
    }
}