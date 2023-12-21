using Newtonsoft.Json;

namespace TechnicalTest.Server
{
    [Serializable]
    public class StudentScores 
    {
        [JsonProperty("StudentID")]
        public int StudentID { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Subject")]
        public string Subject { get; set; }
        [JsonProperty("Scores")]
        public List<Scores> Scores { get; set; }
    }
}
