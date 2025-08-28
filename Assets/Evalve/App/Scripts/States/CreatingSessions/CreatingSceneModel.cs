namespace Evalve.App.States.CreatingSessions
{
    public class CreatingSceneModel : Model
    {
        private readonly Session _session;

        public CreatingSceneModel(Session session)
        {
            _session = session;
        }
        
        public string Log => _session.Log;
    }
}