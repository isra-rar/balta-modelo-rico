using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymenteContext.Domain.Entities;
using PaymenteContext.Domain.ValueObjects;

namespace PaymenteContext.Test.Entities
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void TesteMethod1()
        {
            var name = new Name("Teste", "Teste");
            foreach (var not in name.Notifications)
            {
                Console.WriteLine(not.Message);
                Console.WriteLine(not.Key);
            }
        }
    }
}