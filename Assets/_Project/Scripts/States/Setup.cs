using System.Linq;
using Evalve.Client;
using Evalve.Panels;
using Evalve.Systems;
using SceneObject = Evalve.Panels.SceneObject;

namespace Evalve.States
{
    public class Setup : State
    {
        private readonly Panels.SplashScreen _ui;

        public Setup(StateMachine stateMachine) : base(stateMachine)
        {
            _ui = Services.Get<SceneObject>().Show<Panels.SplashScreen>();
        }

        public override async void Enter()
        {
            Logger.EntryLogged += PrintLog;
            _ui.Confirmed += Confirm;
            _ui.ClearLog();
            _ui.SetConfirmActive(false);
            
            Logger.Log("<color=#bada55>Loading Scene... Please wait.</color>");
            const string url = "http://localhost/api/v1";

            var connection = new Connection(url);
            
            var assetBundle = await connection.GetAssetBundleAsync("01k1fy5qjxd3pvdajv72ty3ze0");
            var assetNames = assetBundle.Elements
                .Select(element => element.Name)
                .ToArray();
            
            await FileDownloader.DownloadFileAsync(assetBundle.Url, assetBundle.Id);
            await AssetBundleLoader.LoadAssetsAsync(assetBundle.Id, assetNames);
            
            Logger.Log("<color=#bada55>Scene Loaded successfully!</color>");
            _ui.SetConfirmActive(true);
        }

        public override void Exit()
        {
            Logger.EntryLogged -= PrintLog;
            _ui.Confirmed -= Confirm;
        }

        public override void Update() { }

        private void PrintLog(string obj)
        {
            _ui.AppendLog(obj);
        }

        private void Confirm()
        {
            _stateMachine.ChangeState<IdleUi>();
        }
    }
}