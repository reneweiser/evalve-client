namespace Evalve.App.States.CreatingSessions
{
    public class CreatingSessionModel : Model
    {
        private readonly Session _session;

        public CreatingSessionModel(Session session)
        {
            _session = session;
        }
        
        public string Email
        {
            get => _session.UserEmail;
            set => _session.UserEmail = value;
        }

        public string Password
        {
            get => _session.UserPassword;
            set => _session.UserPassword = value;
        }

        public bool RememberMe
        {
            get => _session.UserRememberMe;
            set => _session.UserRememberMe = value;
        }
        
        public FormStatus FormStatus { get; set; }
        public string Message { get; set; }
    }
}