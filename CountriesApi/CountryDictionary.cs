namespace CountriesApi
{
    using Countries.Db;
    using Countries.Model;

    public class CountryDictionary : Dictionary<int, PhonePrefixDict>
    {

        private readonly ICountriesService service;

        public CountryDictionary(ICountriesService service)
        {
            this.service = service;
            foreach (PhonePrefix? prefix in service.GetPrefixesAsync().Result)
            {
                int p = Convert.ToInt32(prefix.Prefix);
                if (ContainsKey(p))
                {
                    this[p].Countries.Add(prefix.Country);
                }
                else
                {
                    Add(p, new PhonePrefixDict(prefix));
                }
            }
        }
    }

    public class PhonePrefixDict : PhonePrefix
    {
        public PhonePrefixDict(PhonePrefix prefix)
        {
            PhonePrefixId = prefix.PhonePrefixId;
            Prefix = prefix.Prefix;
            CountryId = prefix.CountryId;
            Countries.Add(prefix.Country);
        }

        public ICollection<Country> Countries { get; set; } = new HashSet<Country>();
    }

}
