using TMPro;
using UnityEngine;

namespace Evalve.App.Ui.Elements
{
    public abstract class Element : MonoBehaviour
    {
        public string Label
        {
            get => _label.text;
            set => _label.text = value;
        }

        [SerializeField] protected TMP_Text _label;
    }
}