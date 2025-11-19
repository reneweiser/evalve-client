using System.Threading.Tasks;
using UnityEngine;

namespace Evalve.App.Ui.Elements
{
    public class Notifications : MonoBehaviour
    {
        [SerializeField] private Notification _notificationPrefab;
        [SerializeField] private Transform _container;

        public async void AddNotification(string notification)
        {
            var item = Instantiate(_notificationPrefab, _container);
            item.Label = notification;
            item.transform.SetAsFirstSibling();

            await Task.Delay(2000);
            
            item.Close();
        }
    }
}