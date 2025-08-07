using System;
using System.Linq;
using System.Threading.Tasks;
using Evalve.Client;
using Evalve.Panels;
using Evalve.Panels.Elements;
using Evalve.SceneObjects;
using Evalve.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneObject = Evalve.SceneObjects.SceneObject;

namespace Evalve.States
{
    public class Setup : State
    {
        private readonly Panels.SplashScreen _ui;
        private readonly AssetManager _assetManager;
        private readonly MenuItem _assetsMenu;
        private readonly MenuItem _objectsMenu;
        private readonly MenuItem _toolsMenu;
        private readonly MenuItem _settings;
        private readonly SessionConfig _session;
        private readonly Connection _connection;
        private readonly ObjectManager _objectsManager;

        public Setup()
        {
            _assetManager = Services.Get<AssetManager>();
            _session = Services.Get<SessionConfig>();
            _connection = Services.Get<Connection>();
            _objectsManager = Services.Get<ObjectManager>();

            var ui = Services.Get<Ui>();
            var menu = ui.Get<ElementsMenu>();
            
            _assetsMenu = menu.CreateMenuItem("Assets");
            _objectsMenu = menu.CreateMenuItem("Objects");
            _toolsMenu = menu.CreateMenuItem("Tools");
            _settings = menu.CreateMenuItem("Settings");
            menu.Show(_assetsMenu);
            
            _ui = ui.Show<Panels.SplashScreen>();
        }

        public override async void Enter()
        {
            Logger.EntryLoggedFormatted += PrintLog;
            AssetBundleLoader.Loaded += AddAssetToMenu;
            // TODO: Use MVP pattern ObjectsManager/ObjectsManagerPresenter/ObjectsManagerView to create objects list in menu
            // _objectsManager.SceneObjectAdded += obj => _objectsMenu.AddCommand(obj.Data.Name, obj.Toggle);
            // _objectsManager.SceneObjectRemoved += obj => _objectsMenu.RemoveCommand(obj.Data.Id);
            _ui.ClearLog();
            _ui.SetConfirmActive(false);
            
            Logger.Log("Loading Scene. Please wait.");

            await Task.WhenAll(_session.AssetIds.Select(HandleAssetLoading));
            await Task.WhenAll(_session.ObjectIds.Select(HandleObjectLoading));
            
            _toolsMenu.AddCommand("Place Object", () => ChangeState(new CreatingObject()));
            _toolsMenu.AddCommand("Select Object", () => ChangeState(new SelectingObject()));
            
            _settings.AddCommand("Reload", () => SceneManager.LoadSceneAsync("test1"));
            
            Logger.LogSuccess("Scene Loaded successfully!");
            
            ChangeState(new UsingElementsMenu());
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
                var obj = factory.CreateFromData(sceneObject);
                _objectsManager.Create(obj);
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