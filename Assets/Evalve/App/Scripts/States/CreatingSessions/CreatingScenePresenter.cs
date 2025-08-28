namespace Evalve.App.States.CreatingSessions
{
    public class CreatingScenePresenter : Presenter<CreatingSceneModel, CreatingSceneView>
    {
        private readonly ISessionManager _sessionManager;

        public CreatingScenePresenter(CreatingSceneModel model,
            CreatingSceneView view) : base(model, view)
        {
        }
    }
}