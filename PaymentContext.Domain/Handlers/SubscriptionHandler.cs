using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public ICommandResult Handler(CreateBoletoSubscriptionCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possivel realizar sua assinatura");
        }

        // Verificar se documentos já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já esta em uso!");
        // Verificar se email já esta está cadastrado
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este E-mail já esta em uso!");
        // Gerar os VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(
            command.Street,
            command.Number,
            command.Neighborhood,
            command.City,
            command.State,
            command.Country,
            command.ZipCode
        );

        // Gerar as Entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(
            command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            command.Payer,
            new Document(command.Number, command.PayerDocumentType),
            address,
            email,
            command.Barcode,
            command.BoletoNumber
        );

        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Agrupar as Validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        // Salvar as informações
        _repository.CreatSubscription(student);

        // Enviar email de boas vindas
        _emailService.Send(
            student.Name.ToString(),
            student.Email.Address,
            "Bem vindo ao balta.io",
            "Sua assinatura foi criada com sucesso!"
        );

        // Retornar informações
        return new CommandResult(true, "Assinarura realizada com sucesso!");
    }
}