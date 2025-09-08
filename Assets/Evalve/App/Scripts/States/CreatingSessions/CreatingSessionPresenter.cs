using System;

namespace Evalve.App.States.CreatingSessions
{
    public class CreatingSessionPresenter : Presenter<CreatingSessionModel, CreatingSessionView>
    {
        private readonly ISessionManager _sessionManager;

        private bool _isSubmitActive;

        public CreatingSessionPresenter(CreatingSessionModel model,
            CreatingSessionView view,
            ISessionManager sessionManager) : base(model, view)
        {
            _sessionManager = sessionManager;
        }

        private FormStatus CheckInput()
        {
            var isValid = !string.IsNullOrEmpty(_model.Email) && !string.IsNullOrEmpty(_model.Password);
            return isValid ? FormStatus.Valid : FormStatus.Invalid;
        }

        public override void Initialize()
        {
            _model.FormStatus = FormStatus.Invalid;
            _model.Email = "test@example.com";
            _model.Password = "password";
            
            _view.Subscribe<ViewEvent>(OnFormUpdated);
            _view.Subscribe<FormConfirmed>(OnFormConfirmed);
            
            base.Initialize();
            Refresh();
        }
        
        private async void OnFormConfirmed(FormConfirmed evnt)
        {
            _model.Message = "Logging you in";
            _model.FormStatus = FormStatus.Submitting;
            
            try
            {
                await _sessionManager.Login(_model.Email, _model.Password);
                _model.Message = "<color=green>Login successful</color>";
                
                OnClosed();
            }
            catch (Exception e)
            {
                _model.FormStatus = FormStatus.Invalid;
                _model.Message = $"<color=red>{e.Message}</color>";
            }
        }

        private void OnFormUpdated(ViewEvent evnt)
        {
            switch (evnt.Key)
            {
                case "email":
                    _model.Email = evnt.Value as string;
                    break;
                case "password":
                    _model.Password = evnt.Value as string;
                    break;
                case "remember_me":
                    _model.RememberMe = (bool)evnt.Value;
                    break;
            }
            
            _model.FormStatus = CheckInput();
        }
    }
}