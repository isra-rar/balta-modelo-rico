using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymenteContext.Domain.ValueObjects;

namespace PaymenteContext.Test.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        // Red, Green, Refactor
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("123", Domain.Enums.EDocumentType.CNPJ);
            Assert.IsTrue(!doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenCNPJIsValid()
        {
            var doc = new Document("61442055000146", Domain.Enums.EDocumentType.CNPJ);
            Assert.IsTrue(doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("123", Domain.Enums.EDocumentType.CPF);
            Assert.IsTrue(!doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenCPFIsValid()
        {
            var doc = new Document("09696765428", Domain.Enums.EDocumentType.CPF);
            Assert.IsTrue(doc.IsValid);
        }
    }
}