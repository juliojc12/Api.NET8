using System.Text.RegularExpressions;

namespace ClienteApi.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }

        private static readonly Regex emailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase );

        private Email() { }

        public Email( string value )
        {
            if (string.IsNullOrWhiteSpace( value ))
            {
                throw new ArgumentException( "Email não pode ser vazio." );
            }

            if (!emailRegex.IsMatch( value ))
            {
                throw new FormatException( "Formato de email inválido" );
            }

            Value = value;
        }
    }
}
