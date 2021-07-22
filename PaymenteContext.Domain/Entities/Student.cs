using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymenteContext.Domain.ValueObjects;
using PaymenteContext.Shared.Entities;

namespace PaymenteContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email, Address address)
        {
            Name = name;
            Document = document;
            Email = email;
            Address = address;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email, address);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = false;
            foreach (var sub in _subscriptions)
            {
                if (sub.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract<Student>()
            .Requires()
            .IsFalse(hasSubscriptionActive, "Student.Subscription", "Você já tem uma assinatura ativa")
            .AreNotEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "Está assinatura não possui Pagamentos")
            );

            _subscriptions.Add(subscription);
        }
    }
}