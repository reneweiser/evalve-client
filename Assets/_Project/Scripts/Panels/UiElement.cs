using UnityEngine;

namespace Evalve.Panels
{
    public abstract class UiElement : MonoBehaviour
    {
        public virtual void SetIsActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}