using System;
using Contacts.ValueObjects;

namespace Contacts.Entities
{
    public class Contact
    {
        public Contact(Guid id, string name, Cellphone cellphone, Email email, Address address)
        {
            if (!ValidateName(name))
                throw new System.Exception("Enter a valid name!");

            Id = id;
            Name = name;
            Cellphone = cellphone;
            Email = email;
            Address = address;
        }

        public static Contact New(string name, Cellphone cellphone, Email email, Address address)
        {
            return new Contact(Guid.NewGuid(), name, cellphone, email, address);
        }

        private bool ValidateName(string name) => name?.Length > 3;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Cellphone Cellphone { get; set; }
        public Email Email { get; set; }
        public Address Address { get; set; }

        public override string ToString()
        {
            return $"{Id.ToString().Substring(0, 8)}   {Name.PadRight(30)}{Cellphone}";
        }
    }
}
