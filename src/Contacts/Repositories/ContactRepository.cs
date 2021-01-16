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
                Contact.New("Angelito Casagrande",
                new Cellphone("(11) 98765-1234"),
                new Email("angelito@test.com"),
                new Address("Rua Test", 123, "Bairro Test", "Cidade Test", "SP")
            ));

            Contacts.Add(
                Contact.New("João Silva",
                new Cellphone("(11) 12345-1234"),
                new Email("joao.silva@test.com"),
                new Address("Rua Joao", 123, "Bairro Joao", "Cidade Joao", "SP")
            ));

            Contacts.Add(
                Contact.New("Maria Antonieta",
                new Cellphone("(11) 9991-4567"),
                new Email("maria.antonieta@test.com"),
                new Address("Rua Maria", 123, "Bairro Maria", "Cidade Maria", "SP")
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
