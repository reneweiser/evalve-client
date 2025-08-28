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
            _team = _panel.Add<Select>("Team");
            _assets = _panel.Add<MultiSelect>("Assets");
            _assets.AllowMultipleSelections = true;
            _objects = _panel.Add<MultiSelect>("Objects");
            _objects.AllowMultipleSelections = true;
            _submit = _panel.Add<Submit>("Confirm");
            
            _team.InputChanged += value => Raise(new FormUpdated { FieldName = "team", Value = value });
            _assets.InputChanged += value => Raise(new FormUpdated { FieldName = "assets", Value = value });
            _objects.InputChanged += value => Raise(new FormUpdated { FieldName = "objects", Value = value });
            _submit.Clicked += () => Raise(new FormConfirmed());
            
            _team.SetOptions(model.Teams);
            _assets.SetOptions(model.Assets);
            _objects.SetOptions(model.Objects);
        }
    }
}