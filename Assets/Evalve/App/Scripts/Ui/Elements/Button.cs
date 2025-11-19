using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Evalve.App.Ui.Elements
{
    public class Button : Element, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action Clicked;
        [SerializeField] private UnityEngine.UI.Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(() => Clicked?.Invoke());
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log($"OnPointerEnter: {eventData.position}");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log($"OnPointerExit: {eventData.position}");
        }
    }
}