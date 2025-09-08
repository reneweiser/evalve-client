using System;
using System.Collections.Generic;
using System.Linq;
using Evalve.App.Tests.Session;
using VContainer;

namespace Evalve.App.States.CreatingSessions
{
    public class SelectingResourcesPresenter : Presenter<SelectingResourcesModel, SelectingResourcesView>
    {
        private readonly StateMachine _stateMachine;
        private readonly IObjectResolver _container;
        private readonly ISessionManager _sessionManager;

        public SelectingResourcesPresenter(
            SelectingResourcesModel model,
            SelectingResourcesView view,
            StateMachine stateMachine,
            IObjectResolver container,
            ISessionManager sessionManager) : base(model, view)
        {
            _stateMachine = stateMachine;
            _container = container;
            _sessionManager = sessionManager;
        }

        public override async void Initialize()
        {
            _view.Subscribe<ViewEvent>(OnFormUpdated);
            _view.Subscribe<FormConfirmed>(OnFormConfirmed);
            await _sessionManager.PullTeams();
            await _sessionManager.PullAssets();
            await _sessionManager.PullObjects();
            
            base.Initialize();
        }

        private async void OnFormUpdated(ViewEvent evnt)
        {
            switch (evnt.Key)
            {
                case "logout":
                    await _sessionManager.Logout();
                    _stateMachine.ChangeState(_container.Resolve<CreatingSession>());
                    break;
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