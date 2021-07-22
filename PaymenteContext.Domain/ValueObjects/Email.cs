using Flunt.Validations;
using PaymenteContext.Shared.ValueObjects;

namespace PaymenteContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;
            
            AddNotifications(new Contract<Email>()
            .Requires()
            .IsEmail(Address, "Email.Address", "E-mail inválido"));
        }
        public string Address { get; private set; }
    }
}