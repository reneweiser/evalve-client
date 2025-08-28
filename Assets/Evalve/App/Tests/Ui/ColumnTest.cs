using System;
using System.Collections.Generic;
using Evalve.App.Ui.Elements;
using UnityEngine;

namespace Evalve.App.Tests
{
    public class ColumnTest : MonoBehaviour
    {
        [SerializeField] private Column _column;

        private void Start()
        {
            var multi = _column.Add<MultiSelect>("MultiSelect");
            multi.InputChanged += input => Debug.Log(string.Join(", ", input));
            multi.AllowMultipleSelections = true;
            multi.SetOptions(new Dictionary<string, string>()
            {
                ["Multi1"] = "Multi 1",
                ["Multi2"] = "Multi 2",
                ["Multi3"] = "Multi 3"
            });
            
            var single = _column.Add<MultiSelect>("SingleSelect");
            single.InputChanged += input => Debug.Log(string.Join(", ", input));
            single.AllowMultipleSelections = false;
            single.SetOptions(new Dictionary<string, string>()
            {
                ["Single1"] = "Single 1",
                ["Single2"] = "Single 2",
                ["Single3"] = "Single 3"
            });
            
            var field = _column.Add<ListButton>("Field");
            field.InputChanged += input => Debug.Log(input);
        }
    }
}
