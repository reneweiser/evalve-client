using TMPro;
using UnityEngine;

namespace Evalve.Panels
{
    public class SceneObjectEdit : UiPanel
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _position;

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public void SetPosition(Vector3 position)
        {
            _position.text = position.ToString();
        }
    }
}