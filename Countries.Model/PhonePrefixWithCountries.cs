namespace Countries.Model
{
    public class PhonePrefixWithCountries : PhonePrefix
    {
        public PhonePrefixWithCountries(PhonePrefix prefix)
        {
            PhonePrefixId = prefix.PhonePrefixId;
            Prefix = prefix.Prefix;
            CountryId = prefix.CountryId;
            Countries.Add(prefix.Country);
        }

        public ICollection<Country> Countries { get; set; } = new HashSet<Country>();
    }

}
