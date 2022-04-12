namespace CountriesApi
{
    using Countries.Model;

    using CountriesApi.Contracts;

    using MediatR;

    public class PhonePrefixDictionary : Dictionary<long, PhonePrefixWithCountries>
    {

        public PhonePrefixDictionary(IMediator mediator)
        {
            foreach (PhonePrefix? prefix in mediator.Send(new PrefixesRequest()).Result)
            {
                long p = Convert.ToInt64(prefix.Prefix);
                if (ContainsKey(p))
                {
                    this[p].Countries.Add(prefix.Country);
                }
                else
                {
                    Add(p, new PhonePrefixWithCountries(prefix));
                }
            }
        }

        public PhonePrefixWithCountries GetPrefix(long phoneNumber)
        {
            while (!ContainsKey(phoneNumber))
            {
                phoneNumber /= 10;
            }

            return this[phoneNumber];
        }
    }

}
