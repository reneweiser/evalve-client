using System;
using System.Collections.Generic;
using System.Linq;
using Evalve.App.Tests.Session;

namespace Evalve.App.States.CreatingSessions
{
    public class SelectingResourcesPresenter : Presenter<SelectingResourcesModel, SelectingResourcesView>
    {
        private readonly ISessionManager _sessionManager;

        public SelectingResourcesPresenter(
            SelectingResourcesModel model,
            SelectingResourcesView view,
            ISessionManager sessionManager) : base(model, view)
        {
            _sessionManager = sessionManager;
        }

        public override async void Initialize()
        {
            _view.Subscribe<FormUpdated>(OnFormUpdated);
            _view.Subscribe<FormConfirmed>(OnFormConfirmed);
            await _sessionManager.PullTeams();
            await _sessionManager.PullAssets();
            await _sessionManager.PullObjects();
            
            base.Initialize();
        }

        private void OnFormUpdated(FormUpdated evnt)
        {
            switch (evnt.FieldName)
            {
                case "team":
                    _model.SelectedTeam = evnt.Value as string;
                    break;
                case "assets":
                    _model.SelectedAssets = evnt.Value as List<string>;
                    break;
                case "objects":
                    _model.SelectedObjects = evnt.Value as List<string>;
                    break;
            }
        }

        private void OnFormConfirmed(FormConfirmed evnt)
        {
            _sessionManager.SelectAssets(_model.SelectedAssets);
            _sessionManager.SelectObjects(_model.SelectedObjects);
            
            OnClosed();
        }
    }
}