using PaymenteContext.Domain.Services;

namespace PaymenteContext.Test.Mocks
{
   public class FakeEmailService : IEmailService
    {
        public void Send(string to, string email, string subject, string body)
        {
            
        }
    }
}