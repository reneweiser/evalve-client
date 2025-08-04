using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evalve.Panels
{
    public class AddPose : UiPanel
    {
        public event Action<string> RoleSelected;
        public event Action PoseAdded;
        public event Action Canceled;
        
        [SerializeField] private TMP_Dropdown _role;
        [SerializeField] private Button _addPose;
        [SerializeField] private Button _back;

        private void Awake()
        {
            _role.ClearOptions();
            _role.options.AddRange(new []
            {
                new TMP_Dropdown.OptionData("Default"),
                new TMP_Dropdown.OptionData("Default HMD"),
                new TMP_Dropdown.OptionData("Moderator"),
                new TMP_Dropdown.OptionData("Participant"),
            });
            
            _role.onValueChanged.AddListener(role => RoleSelected?.Invoke(_role.options[role].text));
            _addPose.onClick.AddListener(() => PoseAdded?.Invoke());
            _back.onClick.AddListener(() => Canceled?.Invoke());
        }

        private void OnDisable()
        {
            RoleSelected = null;
            PoseAdded = null;
            Canceled = null;
        }
    }
}