using System;
using Newtonsoft.Json;

namespace Evalve.Client
{
    public class Pose : Property
    {
        [JsonProperty("role")]
        public string Role;
        [JsonProperty("position")]
        public Vector Position;
        [JsonProperty("rotation")]
        public Vector Rotation;

        [JsonIgnore]
        public bool IsSelected;

        public override void Update(Property p)
        {
            if (p.GetType() != typeof(Pose))
                throw new ArgumentException();
            
            var pose = (Pose)p;
            
            if (pose.Role != null)
                Role = pose.Role;
            if (pose.Position != null)
                Position = pose.Position;
            if (pose.Rotation != null)
                Rotation = pose.Rotation;
        }

        public override object ToDynamic()
        {
            return new
            {
                data = new
                {
                    role = Role,
                    position = Position,
                    rotation = Rotation,
                },
                type = "pose",
            };
        }
    }
}