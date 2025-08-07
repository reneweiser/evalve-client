using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Evalve.Client
{
    public class SceneObject
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("team_id")]
        public string TeamId;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("transform")]
        public Transform Transform;
        [JsonProperty("properties")]
        public Property[] Properties;
    }

    public class Transform
    {
        [JsonProperty("position")]
        public Vector Position;
        [JsonProperty("rotation")]
        public Vector Rotation;
    }

    public class Vector
    {
        [JsonProperty("x")]
        public float X;
        [JsonProperty("y")]
        public float Y;
        [JsonProperty("z")]
        public float Z;
    }

    [JsonConverter(typeof(PropertyConverter))]
    public abstract class Property
    {
        public abstract object ToDynamic();
    }

    public class BimData : Property
    {
        [JsonProperty("cad_id")]
        public int CadId;
        [JsonProperty("survey_point_position")]
        public Vector SurveyPointPosition;

        public override object ToDynamic()
        {
            return new{};
        }
    }
    
    public class Body : Property
    {
        [JsonProperty("position")]
        public Vector Position;
        [JsonProperty("rotation")]
        public Vector Rotation;

        public override object ToDynamic()
        {
            return new{};
        }
    }
    
    public class Checkpoint : Property
    {
        [JsonProperty("perimeter")]
        public float Perimeter;

        public override object ToDynamic()
        {
            return new{};
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
    
    public class PropertyConverter : JsonConverter<Property>
    {
        public override Property ReadJson(JsonReader reader, Type objectType, Property existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            var type = obj["type"]?.ToString();

            Property property = type switch
            {
                "bim_data" => new BimData(),
                "body" => new Body(),
                "checkpoint" => new Checkpoint(),
                "pose" => new Pose(),
                _ => throw new JsonSerializationException($"Unknown property type: {type}")
            };

            serializer.Populate(obj["data"]?.CreateReader()!, property);
            return property;
        }

        public override void WriteJson(JsonWriter writer, Property value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToDynamic());
        }

        public override bool CanWrite => true;
    }

    public static class SceneObjectExtensions
    {
        public static Vector3 ToVector3(this Vector source)
        {
            return new Vector3(source.X, source.Y, source.Z);
        }

        public static Vector ToVector(this Vector3 source)
        {
            return new Vector
            {
                X = source.x,
                Y = source.y,
                Z = source.z
            };
        }
    }
}