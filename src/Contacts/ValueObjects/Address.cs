namespace Contacts.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; }
        public int Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        public Address(string street, int number, string neighborhood, string city, string state)
        {
            if (!ValidateStreet(street))
                throw new System.Exception("Enter a valid street!");

            if (!ValidateNumber(number))
                throw new System.Exception("Enter a valid number!");

            if (!ValidateNeighborhood(neighborhood))
                throw new System.Exception("Enter a valid neighborhood!");

            if (!ValidateCity(city))
                throw new System.Exception("Enter a valid city!");

            if (!ValidateState(state))
                throw new System.Exception("Enter a valid state!");

            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }

        private bool ValidateStreet(string street) => street?.Length > 3;

        private bool ValidateNumber(int number) => number > 0;

        private bool ValidateNeighborhood(string neighborhood) => neighborhood?.Length > 3;

        private bool ValidateCity(string city) => city?.Length > 3;

        private bool ValidateState(string state) => state?.Length == 2;

        public override string ToString()
        {
            return $"{Street}, {Number}, {Neighborhood}, {City}/{State}";
        }
    }
}