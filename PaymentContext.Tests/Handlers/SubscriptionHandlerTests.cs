using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;

[TestClass]
public class SubscriptionHandlerTests
{
    // Red, Green, Refactor 
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    {
        var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
        var command = new CreateBoletoSubscriptionCommand();

        command.FirstName = "Lucas";
        command.LastName = "Rodrigues";
        command.Document = "99999999999";
        command.Email = "email@balta.io2";
        command.Barcode = "123456789";
        command.BoletoNumber = "1234654987";
        command.PaymentNumber = "123121";
        command.PaidDate = DateTime.Now;
        command.ExpireDate = DateTime.Now.AddMonths(1);
        command.Total = 60;
        command.TotalPaid = 60;
        command.Payer = "LUCAS R CORP";
        command.PayerDocument = "12345678911";
        command.PayerDocumentType = EDocumentType.CPF;
        command.PayerEmail = "lucas@dc.com";
        command.Street = "andorinhas";
        command.Number = "147";
        command.Neighborhood = "Grecia";
        command.City = "Floripa";
        command.State = "Espirito Santo";
        command.Country = "Brazil";
        command.ZipCode = "12345678";

        handler.Handler(command);
        Assert.AreEqual(false, handler.Valid);
    }
}