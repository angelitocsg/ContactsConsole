using System;
using System.Collections.Generic;
using System.Linq;
using Contacts.Entities;
using Contacts.Interfaces;

namespace Contacts.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private List<Contact> Contacts { get; set; }

        public ContactRepository()
        {
            Contacts = new List<Contact>();
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

        public Contact GetById(Guid id)
        {
            return Contacts.FirstOrDefault(it => it.Id.Equals(id));
        }

        public IEnumerable<Contact> GetAll()
        {
            return Contacts;
        }
    }
}
