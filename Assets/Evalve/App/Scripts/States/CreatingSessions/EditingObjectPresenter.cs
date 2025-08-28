using Evalve.App.Commands;
using VContainer;

namespace Evalve.App.States.CreatingSessions
{
    public class EditingObjectPresenter : Presenter<EditingObjectModel, EditingObjectView>
    {
        private readonly IObjectResolver _container;
        private readonly StateMachine _stateMachine;
        private readonly IObjectManager _objectManager;
        private readonly string _selectedObjectId;

        public EditingObjectPresenter(EditingObjectModel model,
            EditingObjectView view,
            StateMachine stateMachine,
            IObjectResolver container,
            IObjectManager objectManager) : base(model, view)
        {
            _container = container;
            _stateMachine = stateMachine;
            _objectManager = objectManager;
            
            _selectedObjectId = objectManager.GetSelectedObjectId();
        }

        public override void Initialize()
        {
            _view.Subscribe<FormUpdated>(OnFormUpdated);
            _view.Subscribe<FormConfirmed>(OnFormConfirmed);
            base.Initialize();
        }

        private void OnFormConfirmed(FormConfirmed evnt)
        {
            _container.Resolve<UpdateSelectedSceneObject>().Execute();
            OnClosed();
        }

        private void OnFormUpdated(FormUpdated evnt)
        {
            switch (evnt.FieldName)
            {
                case "name":
                    _objectManager.RenameObject(_objectManager.GetSelectedObjectId(), evnt.Value as string);
                    break;
                case "reset_camera":
                    _container.Resolve<ResetObjectCamera>().Execute();
                    break;
                case "move_object":
                    _stateMachine.ChangeState(_container.Resolve<MovingObject>());
                    break;
                case "create_pose_here":
                    _container.Resolve<CreatePose>().Execute();
                    _stateMachine.ChangeState(_container.Resolve<EditingObject>());
                    break;
                case "select_pose":
                    _objectManager.SelectPose(_selectedObjectId , evnt.Value as string);
                    _stateMachine.ChangeState(_container.Resolve<EditingPose>());
                    break;
            }
            
            Refresh();
        }
    }
}