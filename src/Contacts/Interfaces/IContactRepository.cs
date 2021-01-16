using System;
using System.Collections.Generic;
using Contacts.Entities;

namespace Contacts.Interfaces
{
    public interface IContactRepository
    {
        Guid Add(Contact contact);
        bool Update(Contact contact);
        bool Delete(Guid id);
        Contact GetByPartialId(string id);
        IEnumerable<Contact> GetAll();
        IEnumerable<Contact> GetAllBySearch(string searchText);
    }
}
