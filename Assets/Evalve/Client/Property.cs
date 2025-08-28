using System;
using Newtonsoft.Json;

namespace Evalve.Client
{
    [JsonConverter(typeof(PropertyConverter))]
    public abstract class Property
    {
        [JsonIgnore]
        public readonly string Id = Guid.NewGuid().ToString();

        [JsonIgnore]
        public string ObjectId;
        public abstract object ToDynamic();

        public virtual void Update(Property p) { }
    }
}