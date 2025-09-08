using Evalve.App.States.CreatingSessions;
using Evalve.App.Ui.Elements;

namespace Evalve.App.States
{
    public class IdleView : View<IdleModel>
    {
        public override void Initialize(IdleModel model)
        {
            _panel.Add<Header>("Create");
            
            var createObject = _panel.Add<Button>("New Object");
            createObject.Clicked += () => Raise(ViewEvent.Create("create_object"));
            
            _panel.Add<Header>("Edit");
            foreach (var a in model.ObjectOptions)
            {
                var toggle = _panel.Add<Button>(a.Value);
                toggle.Clicked += () => Raise(ViewEvent.Create("select_object", a.Key));
            }
            
            _panel.Add<Header>("Enable/Disable Assets");
            foreach (var a in model.AssetOptions)
            {
                var toggle = _panel.Add<Toggle>(a.Value);
                toggle.SetValue(model.AssetsActive[a.Key]);
                toggle.InputChanged += value => Raise(ViewEvent.Create("toggle_asset", a.Value, value));
            }
            
            _panel.Add<Header>("Scene");
            var selectResources = _panel.Add<Button>("Exit");
            selectResources.Clicked += () => Raise(ViewEvent.Create("select_resources"));
        }
    }
}