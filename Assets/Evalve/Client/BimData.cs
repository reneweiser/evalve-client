using Newtonsoft.Json;

namespace Evalve.Client
{
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
}