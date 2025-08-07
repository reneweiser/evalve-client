using Evalve.Client;
using Evalve.Systems;

namespace Evalve.SceneObjects
{
    public class Pose : SceneObjectProperty
    {
        public override Property Property => new Client.Pose()
        {
            Role = name,
            Position = transform.position.ToVector(),
            Rotation = transform.eulerAngles.ToVector()
        };
    }
}