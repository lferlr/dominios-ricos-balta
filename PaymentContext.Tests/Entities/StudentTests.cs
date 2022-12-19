using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void AdicionarAssinatura()
    {
        var subscription = new Subscription(null);
        var student = new Student("Lucas", "Rodrigues", "00987365412", "hello@gmail.com");
        student.AddSubscription(subscription);
    }
}