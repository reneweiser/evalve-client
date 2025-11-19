using System.Collections;
using UnityEngine;

namespace Evalve.App.Ui.Elements
{
    public class Notification : Element
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _moveContainer;
        
        public void Close()
        {
            StartCoroutine(FadeTo(.5f));
        }

        private IEnumerator FadeTo(float duration)
        {
            var startAlpha = _canvasGroup.alpha;
            var time = 0f;
            var startPos = _moveContainer.localPosition;

            while (time < duration)
            {
                time += Time.deltaTime;
                var easeT = time * time;
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, easeT / duration);
                _moveContainer.localPosition = Vector3.Lerp(startPos, Vector3.right * 300, easeT / duration);
                yield return null;
            }
            
            Destroy(gameObject);
        }
    }
}