using System;

namespace OnlineOrdering
{
    public class Address
    {
        private string _street;
        private string _city;
        private string _stateOrProvince;
        private string _country;

        public Address(string street, string city, string stateOrProvince, string country)
        {
            _street = street;
            _city = city;
            _stateOrProvince = stateOrProvince;
            _country = country;
        }

        public bool IsInUSA()
        {
            // You can make this smarter if you want, but this meets the spec
            return _country.Trim().ToUpper() == "USA";
        }

        public string GetFullAddress()
        {
            // Street on first line, city+state on second, country on third
            return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
        }
    }
}
