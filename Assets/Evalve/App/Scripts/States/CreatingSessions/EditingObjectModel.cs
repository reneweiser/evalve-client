using System.Collections.Generic;
using System.Linq;

namespace Evalve.App.States.CreatingSessions
{
    public class EditingObjectModel : Model
    {
        public EditingObjectModel(IObjectManager objectManager)
        {
            var selectedObjectId = objectManager.GetSelectedObjectId();

            ObjectName = objectManager.GetObject(selectedObjectId).name;
            ObjectPoses = objectManager.GetPoses(selectedObjectId).ToDictionary(i => i.id, i => i.role);
        }

        public string ObjectName { get; }
        public Dictionary<string, string> ObjectPoses { get; }
    }
}