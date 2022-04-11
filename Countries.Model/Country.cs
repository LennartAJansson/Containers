namespace Countries.Model
{
    public class Country
    {
        public int CountryId { get; set; }
        public string IsoCountry { get; set; }
        public string CountryCode2 { get; set; }
        public string CountryCode3 { get; set; }
        public string CountryName { get; set; }

        public ICollection<PhonePrefix> Prefixes { get; set; } = new HashSet<PhonePrefix>();
    }
}