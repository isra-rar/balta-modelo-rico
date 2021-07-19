using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymenteContext.Domain.Entities;

namespace PaymenteContext.Test.Entities
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void TesteMethod1()
        {
            var subscription = new Subscription(DateTime.Now.AddDays(30));
            var student = new Student("Israel", "Rodrigues", "09696767439", "rodg.isra@gmail.com");
            student.AddSubscription(subscription);
        }
    }
}