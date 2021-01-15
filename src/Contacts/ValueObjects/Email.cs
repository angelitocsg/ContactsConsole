namespace Contacts.ValueObjects
{
    public class Email
    {
        private string _email;

        public Email(string email)
        {
            if (!ValidateEmail(email))
                throw new System.Exception("Enter a valid e-mail!");

            this._email = email;
        }

        private bool ValidateEmail(string email)
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }

        public override string ToString()
        {
            return _email;
        }
    }
}
