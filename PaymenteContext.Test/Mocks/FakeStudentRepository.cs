using PaymenteContext.Domain.Entities;
using PaymenteContext.Domain.Repositories;

namespace PaymenteContext.Test.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {

        }

        public bool DocumentExists(string document)
        {
            if (document == "99999999999")
                return true;

            return false;
        }

        public bool EmailExists(string email)
        {
            if (email == "hello@balta.io")
                return true;

            return false;
        }
    }
}