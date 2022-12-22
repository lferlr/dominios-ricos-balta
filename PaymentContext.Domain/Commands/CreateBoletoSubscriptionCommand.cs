using PaymentContext.Domain.Enums;

namespace PaymentContext.Domain.Commands;

public class CreateBoletoSubscriptionCommand
{
    // Student
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }

    // Payment
    public string Barcode { get; set; }
    public string BoletoNumber { get; set; }

    public string PaymentNumber { get; set; }
    public DateTime PaidDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public decimal Total { get; set; }
    public decimal TotalPaid { get; set; }
    public string Payer { get; set; }

    // Document
    public string PayerDocument { get; set; }
    public EDocumentType PayerDocumentType { get; set; }

    // Email  
    public string PayerEmail { get; set; }

    // Address
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}