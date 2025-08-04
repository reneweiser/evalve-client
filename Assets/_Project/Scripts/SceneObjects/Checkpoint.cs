using UnityEngine;

namespace Evalve.SceneObjects
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private Transform _perimeter;
        
        public void SetPerimeter(float perimeter)
        {
            _perimeter.localScale = new Vector3(perimeter * 2f, .1f, perimeter * 2f);
        }
    }
}