using TMPro;
using UnityEngine;

namespace Evalve.App.Ui.Elements
{
    public class Text : Element
    {
        public float Size
        {
            set => _label.fontSize = value;
        }
    }
}