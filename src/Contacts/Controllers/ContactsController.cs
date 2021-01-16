using System;
using System.Collections.Generic;
using System.Linq;
using Contacts.Entities;
using Contacts.Interfaces;
using Contacts.ValueObjects;
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
            switch (action.KeyChar)
            {
                case '1':
                    ListAll();
                    break;

                case '2':
                    Add();
                    break;

                case '3':
                    Edit();
                    break;

                case '4':
                    Remove();
                    break;

                case '5':
                    ListAll(true);
                    break;
            }
        }

        private void Remove()
        {
            var id = GetId();

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

        private void CreateForm()
        {
            var baseline = 5;
            var currentLine = baseline;
            var labelWidth = 15;

            View.InitialScreen();

            View.CreateTextAtLine(currentLine++, $"{"Name".PadRight(labelWidth)}:");
            View.CreateTextAtLine(currentLine++, $"{"Cellphone".PadRight(labelWidth)}:");
            View.CreateTextAtLine(currentLine++, $"{"Email".PadRight(labelWidth)}:");
            View.CreateTextAtLine(currentLine++, $"{"Street".PadRight(labelWidth)}:");
            View.CreateTextAtLine(currentLine++, $"{"Number".PadRight(labelWidth)}:");
            View.CreateTextAtLine(currentLine++, $"{"Neighborhood".PadRight(labelWidth)}:");
            View.CreateTextAtLine(currentLine++, $"{"City".PadRight(labelWidth)}:");
            View.CreateTextAtLine(currentLine++, $"{"State (2 chars)".PadRight(labelWidth)}:");
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

        private Contact UserInput(bool update = false, Contact contact = null)
        {
            var baseline = 5;
            var currentLine = baseline;
            var labelWidth = 15;
            var inputStart = labelWidth + 4;

            currentLine = baseline;
            Console.SetCursorPosition(inputStart, currentLine++);
            var name = Console.ReadLine();
            name = update && string.IsNullOrWhiteSpace(name) ? contact.Name : name;

            Console.SetCursorPosition(inputStart, currentLine++);
            var cellphone = Console.ReadLine() ?? contact.Cellphone.ToString();
            cellphone = update && string.IsNullOrWhiteSpace(cellphone) ? contact.Cellphone.ToString() : cellphone;

            Console.SetCursorPosition(inputStart, currentLine++);
            var email = Console.ReadLine() ?? contact.Email.ToString();
            email = update && string.IsNullOrWhiteSpace(email) ? contact.Email.ToString() : email;

            Console.SetCursorPosition(inputStart, currentLine++);
            var street = Console.ReadLine() ?? contact.Address.Street;
            street = update && string.IsNullOrWhiteSpace(street) ? contact.Address.Street : street;

            Console.SetCursorPosition(inputStart, currentLine++);
            int number = 0;
            var numberStr = Console.ReadLine();
            numberStr = update && string.IsNullOrWhiteSpace(numberStr) ? contact.Address.Number.ToString() : numberStr;
            int.TryParse(numberStr, out number);

            Console.SetCursorPosition(inputStart, currentLine++);
            var neighborhood = Console.ReadLine() ?? contact.Address.Neighborhood;
            neighborhood = update && string.IsNullOrWhiteSpace(neighborhood) ? contact.Address.Neighborhood : neighborhood;

            Console.SetCursorPosition(inputStart, currentLine++);
            var city = Console.ReadLine() ?? contact.Address.City;
            city = update && string.IsNullOrWhiteSpace(city) ? contact.Address.City : city;

            Console.SetCursorPosition(inputStart, currentLine++);
            var state = Console.ReadLine() ?? contact.Address.State;
            state = update && string.IsNullOrWhiteSpace(state) ? contact.Address.State : state;

            if (update)
            {
                try
                {
                    var contactUpdate = new Contact(contact.Id,
                        name,
                        new Cellphone(cellphone),
                        new Email(email),
                        new Address(street, number, neighborhood, city, state));

                    return contactUpdate;
                }
                catch (Exception ex)
                {
                    View.ShowStatusBarMessage(ex.Message);
                }
            }

            try
            {
                var contactAdd = Contact.New(name,
                    new Cellphone(cellphone),
                    new Email(email),
                    new Address(street, number, neighborhood, city, state));

                return contactAdd;
            }
            catch (Exception ex)
            {
                View.ShowStatusBarMessage(ex.Message);
            }

            return null;
        }

        private void Add()
        {
            var baseline = 5;
            var currentLine = baseline;
            var labelWidth = 15;
            var inputStart = labelWidth + 4;

            CreateForm();

            var contact = UserInput();

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

        private string GetId()
        {
            var baseline = 5;
            var labelWidth = 10;
            var inputStart = labelWidth + 4;

            View.InitialScreen();

            View.CreateTextAtLine(baseline, $"{"Enter Id".PadRight(labelWidth)}:");

            Console.SetCursorPosition(inputStart, baseline);
            return Console.ReadLine();
        }

        private string GetSearchText()
        {
            var baseline = 5;
            var labelWidth = 25;
            var inputStart = labelWidth + 4;

            View.InitialScreen();

            View.CreateTextAtLine(baseline, $"{"Enter text to search".PadRight(labelWidth)}:");

            Console.SetCursorPosition(inputStart, baseline);
            return Console.ReadLine();
        }

        private void Edit()
        {
            var id = GetId();

            var contact = _repository.GetByPartialId(id);

            if (contact == null)
            {
                View.ShowStatusBarMessage($"No contacts found with partial id {id}.");
                return;
            }

            CreateForm();
            PopulateForm(contact);

            var contactUpdated = UserInput(true, contact);

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
            var searchText = GetSearchText();
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
    }
}
