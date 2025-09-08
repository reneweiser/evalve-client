using System.Linq;
using Evalve.App.States.CreatingSessions;
using Evalve.Client;
using Evalve.Contracts;
using UnityEngine.InputSystem;
using VContainer;

namespace Evalve.App.States
{
    public class IdlePresenter : Presenter<IdleModel, IdleView>
    {
        private readonly StateMachine _stateMachine;
        private readonly IObjectResolver _container;
        private readonly IObjectManager _objectManager;
        private readonly IAssetManager _assetManager;

        public IdlePresenter(
            IdleModel model,
            IdleView view,
            StateMachine stateMachine,
            IObjectResolver container,
            IObjectManager objectManager,
            IAssetManager assetManager
        ) : base(model, view)
        {
            _stateMachine = stateMachine;
            _container = container;
            _objectManager = objectManager;
            _assetManager = assetManager;
        }

        public override void Initialize()
        {
            _view.Subscribe<ViewEvent>(OnFormUpdated);
            
            base.Initialize();
        }

        private void OnFormUpdated(ViewEvent eventData)
        {
            switch (eventData.Name)
            {
                case "create_object":
                    var sObject = _objectManager.CreateNewObject();
                    _objectManager.SelectObject(sObject.GetId());
                    _stateMachine.ChangeState(_container.Resolve<MovingObject>());
                    return;
                case "select_object":
                    _objectManager.SelectObject(eventData.Key);
                    _stateMachine.ChangeState(_container.Resolve<EditingObject>());
                    return;
                case "select_resources":
                    _stateMachine.ChangeState(_container.Resolve<CleaningUpScene>());
                    return;
                case "toggle_asset":
                    _model.AssetsActive[eventData.Key] = (bool)eventData.Value;
                    
                    var selectedAssets = _model.AssetsActive
                        .Where(i => i.Value)
                        .Select(i => i.Key)
                        .ToList();
                    
                    _assetManager.SelectAssets(selectedAssets);
                    return;
            }
        }
    }
}