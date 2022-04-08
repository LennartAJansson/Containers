namespace CronJob.Model
{
    using System.Text.Json.Serialization;

    public class CountryFromApi
    {
        [JsonPropertyName("name")]
        public Name Name { get; set; }
        [JsonPropertyName("cca2")]
        public string CountryCode2 { get; set; }

        [JsonPropertyName("cca3")]
        public string CountryCode3 { get; set; }

        [JsonPropertyName("ccn3")]
        public string IsoCountry { get; set; }
        [JsonPropertyName("idd")]
        public Idd PhonePrefix { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("common")]
        public string Common { get; set; }
    }

    public class Idd
    {
        [JsonPropertyName("root")]
        public string Prefix { get; set; }
        [JsonPropertyName("suffixes")]
        public string[] Suffixes { get; set; }
    }
}
