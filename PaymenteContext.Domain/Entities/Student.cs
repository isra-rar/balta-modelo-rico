using System.Collections.Generic;
using System.Linq;
using PaymenteContext.Domain.ValueObjects;
using PaymenteContext.Shared.Entities;

namespace PaymenteContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            foreach (var item in Subscriptions)
                item.Inactivate();

            _subscriptions.Add(subscription);
        }
    }
}