using UnityEngine;

namespace Evalve.Panels
{
    public abstract class UiPanel : MonoBehaviour
    {
        public virtual void SetIsActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}