using PaymenteContext.Domain.Entities;

namespace PaymenteContext.Domain.Repositories
{
    public interface IStudentRepository
    {
         bool DocumentExists(string document);
         bool EmailExists(string email);
         void CreateSubscription(Student student);
    }
}