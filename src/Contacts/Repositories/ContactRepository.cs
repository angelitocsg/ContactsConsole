using System;
using System.Collections.Generic;
using System.Linq;
using Contacts.Entities;
using Contacts.Interfaces;
using Contacts.ValueObjects;

namespace Contacts.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private List<Contact> Contacts { get; set; }

        public ContactRepository()
        {
            Contacts = new List<Contact>();
            Mockup();
        }

        private void Mockup()
        {
            Contacts.Add(
                Contact.New("Google Brasil",
                new Cellphone("(11) 2395-8400"),
                new Email("google@google.com"),
                new Address("Avenida Bridadeiro Faria Lima", 3477, "Itam Bibi", "São Paulo", "SP")
            ));

            Contacts.Add(
                Contact.New("Microsoft Brasil",
                new Cellphone("(11) 5504-2155"),
                new Email("microsoft@microsoft.com"),
                new Address("Avenida Presidente Juscelino Kubitscheck", 1909, "Vila Nova Conceição", "São Paulo", "SP")
            ));

            Contacts.Add(
                Contact.New("SAP Brasil",
                new Cellphone("(11) 5503-2400"),
                new Email("sap@sap.com"),
                new Address("Avenida das Nações Unidas", 14171, "Vila Almeida", "São Paulo", "SP")
            ));
        }

        public Guid Add(Contact contact)
        {
            Contacts.Add(contact);
            return contact.Id;
        }

        public bool Update(Contact contact)
        {
            if (Delete(contact.Id))
            {
                Add(contact);
                return true;
            }

            return false;
        }

        public bool Delete(Guid id)
        {
            var count = Contacts.Count;
            Contacts = Contacts.Where(it => !it.Id.Equals(id)).ToList();

            return count > Contacts.Count();
        }

        public Contact GetByPartialId(string id)
        {
            return Contacts.FirstOrDefault(it => it.Id.ToString().StartsWith(id));
        }

        public IEnumerable<Contact> GetAll()
        {
            return Contacts.OrderBy(it => it.Name);
        }

        public IEnumerable<Contact> GetAllBySearch(string searchText)
        {
            return Contacts.Where(it => it.Name.Contains(searchText)
             || it.Cellphone.ToString().Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
             || it.Email.ToString().Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
             || it.Address.Street.ToString().Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
             || it.Address.Neighborhood.ToString().Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
             || it.Address.City.ToString().Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
             || it.Address.State.ToString().Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
             ).OrderBy(it => it.Name);
        }
    }
}
