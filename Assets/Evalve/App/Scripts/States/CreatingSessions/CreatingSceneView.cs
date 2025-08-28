using Evalve.App.Ui.Elements;

namespace Evalve.App.States.CreatingSessions
{
    public class CreatingSceneView : View<CreatingSceneModel>
    {
        private Text _log;

        public override void Initialize(CreatingSceneModel model)
        {
            _log = _panel.Add<Text>("Log");
        }

        public override void Tick(CreatingSceneModel model)
        {
            _log.Label = model.Log;
        }
    }
}