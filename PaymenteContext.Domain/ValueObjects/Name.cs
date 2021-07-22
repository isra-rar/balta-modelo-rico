using Flunt.Validations;
using PaymenteContext.Shared.ValueObjects;

namespace PaymenteContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract<Name>()
            .Requires()
            .IsGreaterThan(FirstName, 3, "Name,FirstName", "Nome deve conter pelo menos 3 caracteres")
            .IsGreaterThan(LastName, 3, "Name,LastName", "Nome deve conter pelo menos 3 caracteres"));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}