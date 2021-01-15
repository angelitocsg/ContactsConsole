namespace Contacts.ValueObjects
{
    public class Cellphone
    {
        private string _cellphone;

        public Cellphone(string cellphone)
        {
            if (!ValidateCellphone(cellphone))
                throw new System.Exception("Enter a valid cellphone!");

            this._cellphone = cellphone;
        }

        private bool ValidateCellphone(string cellphone)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})");
            return regex.IsMatch(cellphone);
        }

        public override string ToString()
        {
            return _cellphone;
        }

        public string OnlyNumbers()
        {
            return _cellphone
                .Replace(" ", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("-", "");
        }
    }
}
