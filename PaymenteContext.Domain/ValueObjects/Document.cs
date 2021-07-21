using PaymenteContext.Domain.Enums;
using PaymenteContext.Shared.ValueObjects;

namespace PaymenteContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;
        }
        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }


    }
}