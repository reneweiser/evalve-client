using UnityEngine;

namespace Evalve.App.States.CreatingSessions
{
    public class MovingObjectPresenter : Presenter<MovingObjectModel, MovingObjectView>
    {
        private readonly SceneCursor _cursor;

        public MovingObjectPresenter(MovingObjectModel model, MovingObjectView view, SceneCursor cursor) : base(model, view)
        {
            _cursor = cursor;
        }

        public override void Tick()
        {
            _model.ObjectPosition = _cursor.Point;
            base.Tick();
        }
    }

    public class MovingObjectModel : Model
    {
        public Vector3 ObjectPosition { get; set; }
    }
}