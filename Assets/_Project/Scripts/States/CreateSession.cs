using System.Collections.Generic;
using Evalve.Client;
using Evalve.Panels;
using Evalve.Systems;

namespace Evalve.States
{
    public class CreateSession : State
    {
        private readonly SessionConfigurator _ui;
        private readonly Connection _connection;

        private readonly List<string> _assets = new();
        private readonly List<string> _objects = new();

        public CreateSession(StateMachine stateMachine) : base(stateMachine)
        {
            var ui = Services.Get<Ui>();
            _ui = ui.Show<SessionConfigurator>();
            _connection = Services.Get<Connection>();
        }

        public override async void Enter()
        {
            _ui.Confirm += Confirm;
            var assetBundles = await _connection.GetAssetBundlesAsync();
            foreach (var assetBundle in assetBundles)
                _ui.AddAsset(assetBundle.Name, () => AddAsset(assetBundle));

            var objects = await _connection.GetSceneObjectsAsync();
            foreach (var obj in objects)
                _ui.AddObject(obj.Name, () => AddObject(obj));
        }

        public override void Exit() { }

        public override void Update() { }

        private void AddObject(SceneObject sceneObject)
        {
            if (_objects.Contains(sceneObject.Id)) _objects.Remove(sceneObject.Id);
            else _objects.Add(sceneObject.Id);
        }

        private void AddAsset(AssetBundle assetBundle)
        {
            if (_assets.Contains(assetBundle.Id)) _assets.Remove(assetBundle.Id);
            else _assets.Add(assetBundle.Id);
        }

        private void Confirm()
        {
            Services.Register(new SessionConfig()
            {
                AssetIds = _assets.ToArray(),
                ObjectIds = _objects.ToArray(),
            });
            _stateMachine.ChangeState<Setup>();
        }
    }
}