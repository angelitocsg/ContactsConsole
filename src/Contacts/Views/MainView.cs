using System;
using Contacts.Entities;
using Contacts.ValueObjects;

namespace Contacts.Views
{
    public class MainView : Screen
    {
        public MainView()
        {
            InitialScreen();
        }

        public void InitialScreen()
        {
            Lines = 0;
            System.Console.Clear();
            CreateTopLine();
            CreateTextLine("CONTACT LIST");
            CreateMiddleLine();
            CreateTextLine("1) List  2) Add  3) Edit  4) Del  5) Search  6) WhatsApp  7) Maps");
            CreateSingleMiddleLine();

            for (int i = Lines + 3; i < SCREEN_HEIGHT; i++)
                CreateEmptyLine();

            CreateSingleMiddleLine();
            CreateEmptyLine();
            CreateBottomLine();

            SetInputPosition();
        }

        public void SetInputPosition()
        {
            Console.SetCursorPosition(70, 3);
        }

        public void CreateForm()
        {
            var baseline = 5;
            var currentLine = baseline;
            var labelWidth = 15;

            InitialScreen();

            CreateTextAtLine(currentLine++, $"{"Name".PadRight(labelWidth)}:");
            CreateTextAtLine(currentLine++, $"{"Cellphone".PadRight(labelWidth)}:");
            CreateTextAtLine(currentLine++, $"{"Email".PadRight(labelWidth)}:");
            CreateTextAtLine(currentLine++, $"{"Street".PadRight(labelWidth)}:");
            CreateTextAtLine(currentLine++, $"{"Number".PadRight(labelWidth)}:");
            CreateTextAtLine(currentLine++, $"{"Neighborhood".PadRight(labelWidth)}:");
            CreateTextAtLine(currentLine++, $"{"City".PadRight(labelWidth)}:");
            CreateTextAtLine(currentLine++, $"{"State (2 chars)".PadRight(labelWidth)}:");
        }

        public string GetId()
        {
            var baseline = 5;
            var labelWidth = 10;
            var inputStart = labelWidth + 4;

            InitialScreen();

            CreateTextAtLine(baseline, $"{"Enter Id".PadRight(labelWidth)}:");

            Console.SetCursorPosition(inputStart, baseline);
            return Console.ReadLine();
        }

        public string GetSearchText()
        {
            var baseline = 5;
            var labelWidth = 25;
            var inputStart = labelWidth + 4;

            InitialScreen();

            CreateTextAtLine(baseline, $"{"Enter text to search".PadRight(labelWidth)}:");

            Console.SetCursorPosition(inputStart, baseline);
            return Console.ReadLine();
        }

        public Contact UserInput(bool update = false, Contact contact = null)
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
                    ShowStatusBarMessage(ex.Message);
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
                ShowStatusBarMessage(ex.Message);
            }

            return null;
        }
    }
}
