using System;
using System.Linq;
using System.Threading.Tasks;
using Evalve.Client;
using Evalve.Panels;
using Evalve.Panels.Elements;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine;

namespace Evalve.States
{
    public class Setup : State
    {
        private readonly Panels.SplashScreen _ui;
        private readonly AssetManager _assetManager;
        private readonly MenuItem _assetsMenu;
        private readonly MenuItem _objectsMenu;
        private readonly MenuItem _toolsMenu;
        private readonly SessionConfig _session;
        private readonly Connection _connection;

        public Setup(StateMachine stateMachine) : base(stateMachine)
        {
            _assetManager = Services.Get<AssetManager>();
            _session = Services.Get<SessionConfig>();
            _connection = Services.Get<Connection>();

            var ui = Services.Get<Ui>();
            var menu = ui.Get<ElementsMenu>();
            
            _assetsMenu = menu.CreateMenuItem("Assets");
            _objectsMenu = menu.CreateMenuItem("Objects");
            _toolsMenu = menu.CreateMenuItem("Tools");
            
            _ui = ui.Show<Panels.SplashScreen>();
        }

        public override async void Enter()
        {
            Logger.EntryLoggedFormatted += PrintLog;
            AssetBundleLoader.Loaded += AddAssetToMenu;
            _ui.ClearLog();
            _ui.SetConfirmActive(false);
            
            Logger.Log("Loading Scene. Please wait.");

            await Task.WhenAll(_session.AssetIds.Select(HandleAssetLoading));
            await Task.WhenAll(_session.ObjectIds.Select(HandleObjectLoading));
            
            _toolsMenu.AddCommand("Place Object", () => _stateMachine.ChangeState<PlacingObject>());
            _toolsMenu.AddCommand("Select Object", () => _stateMachine.ChangeState<SelectingObject>());
            
            Logger.LogSuccess("Scene Loaded successfully!");
            
            _stateMachine.ChangeState<UsingElementsMenu>();
        }

        public override void Exit()
        {
            Logger.EntryLoggedFormatted -= PrintLog;
            AssetBundleLoader.Loaded -= AddAssetToMenu;
        }

        public override void Update() { }

        private async Task HandleObjectLoading(string id)
        {
            Logger.Log("Loading Objects...");
            try
            {
                var sceneObject = await _connection.GetSceneObjectAsync(id);
                var factory = Services.Get<Factory>();
                var obj = factory.Create(sceneObject);
                _objectsMenu.AddToggleCommand(obj.name, () => obj.Toggle());
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                throw;
            }
        }

        private async Task HandleAssetLoading(string id)
        {
            Logger.Log("Loading Assets...");
            try
            {
                var assetBundle = await _connection.GetAssetBundleAsync(id);
                var assetNames = assetBundle.Elements
                    .Select(element => element.Name)
                    .ToArray();
                await FileDownloader.DownloadFileAsync(assetBundle.Url, assetBundle.Id);
                await AssetBundleLoader.LoadAssetsAsync(assetBundle.Id, assetNames);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                throw;
            }
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