namespace CountriesApi.Mediators
{
    public static class QueryStrings
    {
        public static string SelectCountryByIso = @$"SELECT p.*, c.*
FROM PhonePrefixes AS p
JOIN Countries AS c 
ON p.CountryId = c.CountryId
WHERE c.IsoCountry = @p1";

        public static string SelectCountryByCode2 = @$"SELECT p.*, c.*
FROM PhonePrefixes AS p
JOIN Countries AS c 
ON p.CountryId = c.CountryId
WHERE c.CountryCode2 = @p1";

        public static string SelectCountryByCode3 = @$"SELECT p.*, c.*
FROM PhonePrefixes AS p
JOIN Countries AS c 
ON p.CountryId = c.CountryId
WHERE c.CountryCode3 = @p1";

        public static readonly string SelectAllCountries = @$"SELECT p.*, c.*
FROM PhonePrefixes AS p
JOIN Countries AS c 
ON p.CountryId = c.CountryId";

        public static string SelectAllPrefixes = @"SELECT p.*, c.*
FROM PhonePrefixes AS p 
JOIN Countries AS c 
ON p.CountryId = c.CountryId
ORDER BY p.Prefix";
    }
}
