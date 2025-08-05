using System.Linq;
using System.Threading.Tasks;
using Evalve.Client;
using Evalve.Panels;
using Evalve.Panels.Elements;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine;
using AssetBundle = Evalve.Client.AssetBundle;
using SceneObject = Evalve.Panels.SceneObject;

namespace Evalve.States
{
    public class Setup : State
    {
        private readonly Panels.SplashScreen _ui;
        private readonly AssetManager _assetManager;
        private readonly MenuItem _assetsMenu;
        private readonly MenuItem _objectsMenu;
        private readonly MenuItem _toolsMenu;

        public Setup(StateMachine stateMachine) : base(stateMachine)
        {
            _assetManager = Services.Get<AssetManager>();
            
            var menu = Services.Get<SceneObject>().Show<ElementsMenu>();
            _assetsMenu = menu.CreateMenuItem("Assets");
            _objectsMenu = menu.CreateMenuItem("Objects");
            _toolsMenu = menu.CreateMenuItem("Tools");
            
            _ui = Services.Get<SceneObject>().Show<Panels.SplashScreen>();
        }

        public override async void Enter()
        {
            Logger.EntryLogged += PrintLog;
            _ui.ClearLog();
            _ui.SetConfirmActive(false);
            
            Logger.Log("<color=#bada55>Loading Scene... Please wait.</color>");
            
            var connection = new Connection("http://localhost/api/v1");
            
            await HandleAssetLoading(connection);
            await HandleObjectLoading(connection);
            
            _toolsMenu.AddCommand("Place Object", () => _stateMachine.ChangeState<PlacingObject>());
            _toolsMenu.AddCommand("Select Object", () => _stateMachine.ChangeState<SelectingObject>());
            
            Logger.Log("<color=#bada55>Scene Loaded successfully!</color>");
            
            _stateMachine.ChangeState<UsingElementsMenu>();
        }

        public override void Exit()
        {
            Logger.EntryLogged -= PrintLog;
        }

        public override void Update() { }

        private async Task HandleObjectLoading(Connection connection)
        {
            var sceneObjects = await connection.GetSceneObjectsAsync();
            var factory = Services.Get<Factory>();
            foreach (var sceneObject in sceneObjects)
            {
                var obj = factory.Create(sceneObject);
                _objectsMenu.AddToggleCommand(obj.name, () => obj.Toggle());
            }
        }

        private async Task HandleAssetLoading(Connection connection)
        {
            var assetBundle = await connection.GetAssetBundleAsync("01k1fy5qjxd3pvdajv72ty3ze0");
            var assetNames = assetBundle.Elements
                .Select(element => element.Name)
                .ToArray();
            AssetBundleLoader.Loaded += AddAssetToMenu;
            await FileDownloader.DownloadFileAsync(assetBundle.Url, assetBundle.Id);
            await AssetBundleLoader.LoadAssetsAsync(assetBundle.Id, assetNames);
            AssetBundleLoader.Loaded -= AddAssetToMenu;
        }

        private void AddAssetToMenu(GameObject asset)
        {
            _assetManager.RegisterAsset(asset);
            _assetsMenu.AddToggleCommand(asset.name, () => _assetManager.Toggle(asset));
        }

        private void PrintLog(string obj)
        {
            _ui.AppendLog(obj);
        }
    }
}