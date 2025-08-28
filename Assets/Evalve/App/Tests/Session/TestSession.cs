using System.Linq;
using Evalve.App.Ui.Elements;
using Evalve.Client;
using UnityEngine;
using UnityEngine.SceneManagement;
using ILogger = Evalve.Contracts.ILogger;

namespace Evalve.App.Tests.Session
{
    public class TestSession : MonoBehaviour
    {
        [SerializeField] private Column _login;
        [SerializeField] private Column _selectTeam;
        [SerializeField] private Column _selectResources;
        private SessionManager _sessionManager;
        
        private string _email;
        private string _password;

        private void Start()
        {
            ILogger logger = new EvalveLogger();
            IConnection connection = new Connection("http://localhost/api", logger);
            App.Session session = new App.Session();
            
            _sessionManager = new SessionManager(connection, session);
            
            SetupLogin();
        }

        private void SetupLogin()
        {
            var email = _login.Add<TextInput>("Email");
            email.InputChanged += input => _email = input;
            email.SetValue("test@exampl.com");
            
            var password = _login.Add<TextInput>("Password");
            password.InputChanged += input => _password = input;
            password.SetValue("password");
            
            var submit = _login.Add<Submit>("Submit");
            submit.Clicked += Login;
        }

        private async void Login()
        {
            await _sessionManager.Login(_email, _password);
            
            SetupSelectTeam();
        }

        private void SetupSelectTeam()
        {
            var select = _selectTeam.Add<Select>("Select Team");
            var options = _sessionManager.Session.UserTeams
                .ToDictionary(item => item.Key, item => item.Value.Name);
            select.SetOptions(options);
            select.InputChanged += SetupSelectResources;
            
            _selectTeam.gameObject.SetActive(true);
        }

        private async void SetupSelectResources(string obj)
        {
            _sessionManager.SelectTeam(obj);
            await _sessionManager.PullUserData();
            
            var assets = _selectResources.Add<MultiSelect>("Select Assets");
            assets.AllowMultipleSelections = true;
            
            var assetOptions = _sessionManager.Session.UserAssets
                .ToDictionary(item => item.Key, item => item.Value.Name);
            
            assets.SetOptions(assetOptions);
            
            var objects = _selectResources.Add<MultiSelect>("Select Objects");
            objects.AllowMultipleSelections = true;
            
            var objectOptions = _sessionManager.Session.UserSceneObjects
                .ToDictionary(item => item.Key, item => item.Value.Name);
            
            objects.SetOptions(objectOptions);
            
            var logout = _selectResources.Add<Button>("Logout");
            logout.Clicked += Logout;
            
            _selectResources.gameObject.SetActive(true);
        }

        private async void Logout()
        {
            await _sessionManager.Logout();
            SceneManager.LoadScene("Evalve/App/Tests/Session/TestSession");
        }
    }
}
