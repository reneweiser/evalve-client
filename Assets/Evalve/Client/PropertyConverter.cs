using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Evalve.Client
{
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
}