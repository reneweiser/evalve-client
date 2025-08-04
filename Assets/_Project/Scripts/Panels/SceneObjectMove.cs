using TMPro;
using UnityEngine;

namespace Evalve.Panels
{
    public class SceneObjectMove : UiPanel
    {
        [SerializeField] private TMP_Text _position;

        public void SetPosition(Vector3 position)
        {
            _position.text = position.ToString();
        }
    }
}