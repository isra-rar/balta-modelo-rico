using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymenteContext.Domain.Entities;
using PaymenteContext.Domain.ValueObjects;

namespace PaymenteContext.Test.Entities
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;

        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("09696785475", Domain.Enums.EDocumentType.CPF);
            _email = new Email("bat.wayne@gmail.com");
            _address = new Address("Rua 1", "1234", "Bairro Legal", "Gothan", "SP", "BR", "58052458");
            _student = new Student(_name, _document, _email, _address);
            
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5),
             10, 10, "WAYNE CORP", _document, _address, _email);

            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);
            _student.AddSubscription(subscription);

            Assert.IsTrue(!_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscriptionHasNoPayment()
        {
            var subscription = new Subscription(null);
            _student.AddSubscription(subscription);

            Assert.IsTrue(!_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddActiveSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5),
             10, 10, "WAYNE CORP", _document, _address, _email);

            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.IsValid);
        }
    }
}