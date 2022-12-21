using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
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

    public Name Name { get; set; }
    public Document Document { get; set; }
    public Email Email { get; set; }
    public Address Address { get; set; }
    public IReadOnlyCollection<Subscription> Subscriptions
    {
      get { return _subscriptions.ToArray(); }
    }

    public void AddSubscription(Subscription subscription)
    {
      var hasSubscriptionActive = false;
      // Cancela todas as outras assinaturas e coloca esta como principal
      foreach (var sub in _subscriptions)
      {
        if (sub.Active)
          hasSubscriptionActive = true;  
      }
      
      AddNotifications(new Contract()
        .Requires()
        .IsFalse(hasSubscriptionActive, "Student.Subscriprions", "Você já tem uma assinatura ativa")
        .AreNotEquals(0, subscription.Payment.Count, "Student.Subscription.Payments", "Essa assinatura não possui pagamentos")
      ); 

      if (Valid)
        _subscriptions.Add(subscription);
      // Outra alternativa de Notificação
      // if (hasSubscriptionActive)
      //   AddNotification("Student.Subscriprions", "Você já tem uma assinatura ativa");
    }
  }
}