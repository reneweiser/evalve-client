using System;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Evalve.App
{
    public abstract class Presenter<TModel, TView> : ITickable
        where TModel : Model
        where TView : View<TModel>
    {
        public event Action Closed;
        protected readonly TModel _model;
        protected readonly TView _view;

        protected Presenter(TModel model, TView view)
        {
            _model = model;
            _view = view;
        }
        
        public void SetViewVisible(bool isVisible)
        {
            _view.SetVisible(isVisible);
        }

        public virtual void Initialize()
        {
            _view.Initialize(_model);
        }

        public virtual void Refresh()
        {
            _view.Refresh(_model);
        }

        public virtual void Tick()
        {
            _view.Tick(_model);
        }

        public virtual void Cleanup()
        {
            if (_view == null)
                throw new Exception("View already cleaned up.");
            
            _view.Cleanup();
            Object.Destroy(_view.gameObject);
        }

        protected void OnClosed()
        {
            Closed?.Invoke();
        }
    }
}