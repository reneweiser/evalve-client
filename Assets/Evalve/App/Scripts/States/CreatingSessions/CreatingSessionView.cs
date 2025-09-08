using Evalve.App.Ui.Elements;
using Toggle = Evalve.App.Ui.Elements.Toggle;

namespace Evalve.App.States.CreatingSessions
{
    public class CreatingSessionView : View<CreatingSessionModel>
    {
        private TextInput _email;
        private TextInput _password;
        private Toggle _rememberMe;
        private Submit _confirm;
        private Text _message;

        public override void Initialize(CreatingSessionModel model)
        {
            _email = _panel.Add<TextInput>("Email");
            _password = _panel.Add<TextInput>("Password");
            _rememberMe = _panel.Add<Toggle>("Remember me?");
            _confirm = _panel.Add<Submit>("Login");
            _message = _panel.Add<Text>("");
            
            _email.SetType(TextInputType.Email);
            _password.SetType(TextInputType.Password);

            _email.InputChanged += value => Raise(new ViewEvent { Key = "email", Value = value });
            _password.InputChanged += value => Raise(new ViewEvent { Key = "password", Value = value });
            _rememberMe.InputChanged += value => Raise(new ViewEvent { Key = "remember_me", Value = value.ToString() });
            _confirm.Clicked += () => Raise(new FormConfirmed());
        }

        public override void Refresh(CreatingSessionModel model)
        {
            _email.SetValue(model.Email);
            _password.SetValue(model.Password);
        }

        public override void Tick(CreatingSessionModel model)
        {
            _confirm.SetIsInteractable(model.FormStatus == FormStatus.Valid);
            _message.Label = model.Message;
        }

        public override void Cleanup()
        {
            _panel.Clear();
        }
    }
}