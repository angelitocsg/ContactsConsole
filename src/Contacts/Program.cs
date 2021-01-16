using Contacts.Controllers;
using Contacts.Interfaces;
using Contacts.Repositories;
using Contacts.Services;

namespace Contacts
{
    class Program
    {
        static IContactRepository repository;
        static IOpenLinkService openLink;

        static void Main(string[] args)
        {
            // Init
            repository = new ContactRepository();
            openLink = new OpenLinkService();

            new ContactsController(repository, openLink);
        }
    }
}
