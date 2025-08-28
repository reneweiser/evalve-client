using Evalve.App.Ui.Elements;
using UnityEngine;

namespace Evalve.App.States.CreatingSessions
{
    public class MovingObjectView : View<MovingObjectModel>
    {
        private Text _position;

        public override void Initialize(MovingObjectModel model)
        {
            _panel.Clear();
            _panel.Add<Header>("Moving Object");

            var description = "<color=green>[LeftClick]</color> Confirm position";
            description += "\n<color=green>[Esc]</color> Cancel";
            _panel.Add<Text>(description);
            
            _position = _panel.Add<Text>("Position");
        }

        public override void Tick(MovingObjectModel model)
        {
            _position.Label = model.ObjectPosition.ToString();
        }

        public override void Cleanup()
        {
            _panel.Clear();
        }
    }
}