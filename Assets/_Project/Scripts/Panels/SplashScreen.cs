using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evalve.Panels
{
    public class SplashScreen : UiPanel
    {
        public event Action Confirmed;
        
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _log;
        [SerializeField] private Button _confirmButton;

        private void Awake()
        {
            _confirmButton.onClick.AddListener(() => Confirmed?.Invoke());
        }

        public void SetConfirmActive(bool isActive)
        {
            _confirmButton.gameObject.SetActive(isActive);
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public void ClearLog()
        {
            _log.text = "";
        }

        public void AppendLog(string log)
        {
            _log.text += "\n" + log;
        }
    }
}