using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Pose = Evalve.SceneObjects.Pose;

namespace Evalve.Panels.Elements
{
    public class PoseElement : UiElement
    {
        public event Action ShowSelected;
        public event Action DeleteSelected;
        
        [SerializeField] private TMP_Text _role;
        [SerializeField] private Button _show;
        [SerializeField] private Button _delete;

        private void Awake()
        {
            _show.onClick.AddListener(delegate { ShowSelected?.Invoke(); });
            _delete.onClick.AddListener(delegate { DeleteSelected?.Invoke(); });
        }

        public void Refresh(Pose pose)
        {
            _role.text = pose.name;
        }
    }
}