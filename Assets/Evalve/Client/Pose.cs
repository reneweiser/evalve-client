using System;
using Newtonsoft.Json;

namespace Evalve.Client
{
    public class PollingField : Property
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("image")]
        public string Image;
        [JsonProperty("size")]
        public Resolution Size;
        [JsonProperty("position")]
        public Vector Position;
        [JsonProperty("rotation")]
        public Vector Rotation;
        public override object ToDynamic()
        {
            return new
            {
                data = new
                {
                    name = Name,
                    image = Image,
                    size = Size,
                    position = Position,
                    rotation = Rotation
                },
                type = "pollingField"
            };
        }
    }

    public class CgData : Property
    {
        [JsonProperty("order")]
        public int Order;
        [JsonProperty("fixtureReference")]
        public string FixtureReference;
        [JsonProperty("dwellTime")]
        public int DwellTime;
        [JsonProperty("passthrough")]
        public int Passthrough;
        public override object ToDynamic()
        {
            return new
            {
                data = new
                {
                    order = Order,
                    fixtureReference = FixtureReference,
                    dwellTime = DwellTime,
                    passthrough = Passthrough
                },
                type = "cgData"
            };
        }
    }

    public class Models : Property
    {
        [JsonProperty("models")]
        public string[] Names;
        public override object ToDynamic()
        {
            return new
            {
                data = new
                {
                    models = Names
                },
                type = "models"
            };
        }
    }

    public class Notes : Property
    {
        [JsonProperty("notes")]
        public string Text;
        
        public override object ToDynamic()
        {
            return new
            {
                data = new
                {
                    notes = Text
                },
                type = "notes"
            };
        }
    }

    public class Question : Property
    {
        [JsonProperty("questionId")]
        public string QuestionId;
        [JsonProperty("models")]
        public string[] Models;
        [JsonProperty("size")]
        public Resolution Size;
        [JsonProperty("position")]
        public Vector Position;
        [JsonProperty("rotation")]
        public Vector Rotation;
        public override object ToDynamic()
        {
            return new
            {
                data = new
                {
                    questionId = QuestionId,
                    models = Models,
                    size = Size,
                    position = Position,
                    rotation = Rotation
                },
                type = "question"
            };
        }
    }
    
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