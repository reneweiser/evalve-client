using TMPro;
using UnityEngine;

namespace Evalve.Panels
{
    public class Info : UiPanel
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _label;

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public void SetLabel(string label)
        {
            _label.text = label;
        }
    }
}