using System.Collections.Generic;
using Evalve.App.Ui.Elements;

namespace Evalve.App.States.CreatingSessions
{
    public class SelectingResourcesView : View<SelectingResourcesModel>
    {
        private Select _team;
        private MultiSelect _assets;
        private MultiSelect _objects;
        private Submit _submit;

        public override void Initialize(SelectingResourcesModel model)
        {
            Refresh(model);
        }

        public override void Refresh(SelectingResourcesModel model)
        {
            _panel.Clear();
            var logout = _panel.Add<Button>("Logout");
            logout.Clicked += () => Raise(new ViewEvent {Key = "logout"});
            
            _panel.Add<Header>("Resources");
            _team = _panel.Add<Select>("Team");
            _assets = _panel.Add<MultiSelect>("Assets");
            _assets.AllowMultipleSelections = true;
            _objects = _panel.Add<MultiSelect>("Objects");
            _objects.AllowMultipleSelections = true;
            _submit = _panel.Add<Submit>("Load Scene");
            
            _team.InputChanged += value => Raise(new ViewEvent { Key = "team", Value = value });
            _assets.InputChanged += value => Raise(new ViewEvent { Key = "assets", Value = value });
            _objects.InputChanged += value => Raise(new ViewEvent { Key = "objects", Value = value });
            _submit.Clicked += () => Raise(new FormConfirmed());
            
            _team.SetOptions(model.Teams);
            _team.SetValue(model.SelectedTeam);
            _assets.SetOptions(model.Assets);
            _objects.SetOptions(model.Objects);
        }
    }
}