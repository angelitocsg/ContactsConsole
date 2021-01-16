using System;
using System.Collections.Generic;
using System.Linq;
using Contacts.Entities;
using Contacts.Enumerations;
using Contacts.Interfaces;
using Contacts.Views;

namespace Contacts.Controllers
{
    public class ContactsController
    {
        private readonly IContactRepository _repository;
        private readonly IOpenLinkService _openLink;

        private MainView View;

        public ContactsController(IContactRepository repository, IOpenLinkService openLink)
        {
            _repository = repository;
            _openLink = openLink;

            View = new MainView();

            while (true)
            {
                View.SetInputPosition();
                var action = Console.ReadKey();

                // Exit
                if (action.Key == ConsoleKey.F10)
                    Environment.Exit(0);

                // Restart
                if (action.Key == ConsoleKey.Escape)
                {
                    View = new MainView();
                    continue;
                }

                MenuActions(action);
            }
        }

        private void MenuActions(ConsoleKeyInfo action)
        {
            switch ((MenuAction)action.KeyChar)
            {
                case MenuAction.List:
                    ListAll();
                    break;

                case MenuAction.Add:
                    Add();
                    break;

                case MenuAction.Edit:
                    Edit();
                    break;

                case MenuAction.Remove:
                    Remove();
                    break;

                case MenuAction.Search:
                    ListAll(true);
                    break;

                case MenuAction.WhatsApp:
                    OpenLink(LinkType.WhatsApp);
                    break;

                case MenuAction.GoogleMaps:
                    OpenLink(LinkType.GoogleMaps);
                    break;
            }
        }

        private void Remove()
        {
            var id = View.GetId();

            var contact = _repository.GetByPartialId(id);

            if (contact == null)
            {
                View.ShowStatusBarMessage($"No contacts found with partial id {id}.");
                return;
            }

            if (_repository.Delete(contact.Id))
            {
                View.InitialScreen();
                View.ShowStatusBarMessage($"Contact with id {id} removed.");
            }
        }

        private void PopulateForm(Contact contact)
        {
            var baseline = 5;
            var currentLine = baseline;
            var labelWidth = 15;
            var inputStart = labelWidth + 4;

            Console.SetCursorPosition(inputStart, currentLine++);
            Console.Write(contact.Name);

            Console.SetCursorPosition(inputStart, currentLine++);
            Console.Write(contact.Cellphone);

            Console.SetCursorPosition(inputStart, currentLine++);
            Console.Write(contact.Email);

            Console.SetCursorPosition(inputStart, currentLine++);
            Console.Write(contact.Address.Street);

            Console.SetCursorPosition(inputStart, currentLine++);
            Console.Write(contact.Address.Number);

            Console.SetCursorPosition(inputStart, currentLine++);
            Console.Write(contact.Address.Neighborhood);

            Console.SetCursorPosition(inputStart, currentLine++);
            Console.Write(contact.Address.City);

            Console.SetCursorPosition(inputStart, currentLine++);
            Console.Write(contact.Address.State);
        }

        private void Add()
        {
            var baseline = 5;
            var currentLine = baseline;
            var labelWidth = 15;
            var inputStart = labelWidth + 4;

            View.CreateForm();

            var contact = View.UserInput();

            if (contact == null)
                return;

            try
            {
                var id = _repository.Add(contact);

                View.InitialScreen();
                View.ShowStatusBarMessage($"Contact created with id {id}.");
            }
            catch (Exception ex)
            {
                View.ShowStatusBarMessage(ex.Message);
            }
        }


        private void Edit()
        {
            var id = View.GetId();

            var contact = _repository.GetByPartialId(id);

            if (contact == null)
            {
                View.ShowStatusBarMessage($"No contacts found with partial id {id}.");
                return;
            }

            View.CreateForm();
            PopulateForm(contact);

            var contactUpdated = View.UserInput(true, contact);

            if (contactUpdated == null)
                return;

            if (_repository.Update(contactUpdated))
            {
                View.InitialScreen();
                View.ShowStatusBarMessage($"Contact with id {id} updated.");
            }
        }

        private IEnumerable<Contact> Search()
        {
            var searchText = View.GetSearchText();
            return _repository.GetAllBySearch(searchText);
        }

        private void ListAll(bool fromSearch = false)
        {
            View.InitialScreen();

            var contacts = fromSearch ? Search() : _repository.GetAll();

            if (contacts == null || contacts.Count() == 0)
            {
                View.ShowStatusBarMessage("No contacts found.");
                return;
            }

            var baseline = 5;
            var line = 0;
            View.ShowAsList(baseline, line++, $"{"Id".PadRight(11)}{"Name".PadRight(30)}{"Cellphone"}");
            View.CreateSingleMiddleLine(false); line++;
            contacts.ToList().ForEach(it => View.ShowAsList(baseline, line++, it.ToString()));
        }

        private void OpenLink(LinkType linkType)
        {
            var id = View.GetId();

            var contact = _repository.GetByPartialId(id);

            if (contact == null)
            {
                View.ShowStatusBarMessage($"No contacts found with partial id {id}.");
                return;
            }

            View.InitialScreen();

            switch (linkType)
            {
                case LinkType.GoogleMaps:
                    View.ShowStatusBarMessage($"Opening Google Maps for contact address.");
                    _openLink.GoogleMaps(contact.Address);
                    break;

                case LinkType.WhatsApp:
                    View.ShowStatusBarMessage($"Opening WhatsApp chat for contact cellphone.");
                    _openLink.WhatsAppChat(contact.Cellphone);
                    break;
            }
        }

    }
}
