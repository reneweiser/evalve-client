using Evalve.Client;
using UnityEngine;

namespace Evalve.App
{
    public class SceneObjectProperty : MonoBehaviour
    {
        private Property _property;

        public void SetProperty(Property property)
        {
            _property = property;
        }

        public Property GetProperty()
        {
            return _property;
        }
    }
}