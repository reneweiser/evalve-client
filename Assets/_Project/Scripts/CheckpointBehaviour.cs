using UnityEngine;

namespace Evalve
{
    public class CheckpointBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _perimeter;
        
        public void SetPerimeter(float perimeter)
        {
            _perimeter.localScale = new Vector3(perimeter * 2f, .1f, perimeter * 2f);
        }
    }
}