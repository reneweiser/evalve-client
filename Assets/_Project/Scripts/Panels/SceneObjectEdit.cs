using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evalve.Panels
{
    public class SceneObjectEdit : UiPanel
    {
        public event Action MoveSelected;
        public event Action AddPoseSelected;
        public event Action DeleteSelected;
        public event Action Canceled;
        
        [SerializeField] private TMP_Text _position;
        [SerializeField] private Button _move;
        [SerializeField] private Button _addPose;
        [SerializeField] private Button _delete;
        [SerializeField] private Button _cancel;

        public void SetPosition(Vector3 position)
        {
            _position.text = position.ToString();
        }

        private void Awake()
        {
            _move.onClick.AddListener(() => MoveSelected?.Invoke());
            _addPose.onClick.AddListener(() => AddPoseSelected?.Invoke());
            _delete.onClick.AddListener(() => DeleteSelected?.Invoke());
            _cancel.onClick.AddListener(() => Canceled?.Invoke());
        }

        private void OnDisable()
        {
            MoveSelected = null;
            AddPoseSelected = null;
            DeleteSelected = null;
            Canceled = null;
        }
    }
}