using Flunt.Validations;
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

            AddNotifications(new Contract<Document>()
            .Requires()
            .IsTrue(Validate(), "Document.Number", "Documento Inválido"));
        }
        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }

        private bool Validate()
        {
            if (Type == EDocumentType.CNPJ && Number.Length == 14)
                return true;
            if (Type == EDocumentType.CPF && Number.Length == 11)
                return true;

            return false;
        }

    }
}