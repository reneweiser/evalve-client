using System.Collections.Generic;
using Evalve.App.Commands;
using VContainer;

namespace Evalve.App.States.CreatingSessions
{
    public class EditingPosePresenter : Presenter<EditingPoseModel, EditingPoseView>
    {
        private readonly StateMachine _stateMachine;
        private readonly IObjectManager _objectManager;
        private readonly IObjectResolver _container;

        public EditingPosePresenter(
            EditingPoseModel model,
            EditingPoseView view,
            StateMachine stateMachine,
            IObjectManager objectManager,
            IObjectResolver container) : base(model, view)
        {
            _stateMachine = stateMachine;
            _objectManager = objectManager;
            _container = container;
        }

        public override void Initialize()
        {
            _view.Subscribe<FormUpdated>(OnFormUpdated);
            _view.Subscribe<FormConfirmed>(OnFormConfirmed);
            
            base.Initialize();
        }

        private void OnFormUpdated(FormUpdated evnt)
        {
            switch (evnt.FieldName)
            {
                case "role_changed":
                    _objectManager.SetPoseRole(_objectManager.GetSelectedObjectId(), _objectManager.GetSelectedPoseId(), evnt.Value as string);
                    _container.Resolve<UpdateSelectedSceneObject>().Execute();
                    break;
                case "move_pose_here":
                    _container.Resolve<MovePoseToAvatar>().Execute();
                    break;
                case "move_to_pose":
                    _container.Resolve<MoveAvatarToPose>().Execute();
                    break;
                case "deleted":
                    _container.Resolve<DeleteSelectedPose>().Execute();
                    _stateMachine.ChangeState(_container.Resolve<EditingObject>());
                    break;
            }
        }

        private void OnFormConfirmed(FormConfirmed evnt)
        {
            _objectManager.SelectPose(_objectManager.GetSelectedObjectId(), null);
            _container.Resolve<UpdateSelectedSceneObject>().Execute();
            OnClosed();
        }
    }
}