using Newtonsoft.Json;

namespace TechnicalTest.Server
{
    [Serializable]
    public class Scores
    {
        [JsonProperty("LearningObjective")]
        public string LearningObjective { get; set; }

        [JsonProperty("Score")]
        public string Score { get; set; }
    }
}
