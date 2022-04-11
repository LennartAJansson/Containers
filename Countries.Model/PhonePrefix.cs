namespace Countries.Model
{
    public class PhonePrefix
    {
        public int PhonePrefixId { get; set; }
        public int CountryId { get; set; }
        public string Prefix { get; set; }

        public Country Country { get; set; }
    }
}