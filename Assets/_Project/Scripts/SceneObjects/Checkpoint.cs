using Evalve.Client;
using Evalve.Systems;
using UnityEngine;
using Transform = UnityEngine.Transform;

namespace Evalve.SceneObjects
{
    public class Checkpoint : SceneObjectProperty
    {
        public override Property Property => new Client.Checkpoint()
        {
            Perimeter = _perimeter.localScale.x
        };
        
        [SerializeField] private Transform _perimeter;
        
        public void SetPerimeter(float perimeter)
        {
            _perimeter.localScale = new Vector3(perimeter * 2f, .1f, perimeter * 2f);
        }
    }
}