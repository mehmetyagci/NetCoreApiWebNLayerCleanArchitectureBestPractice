namespace App.Domain.Events;

// Immutable yapıyoruz. Değiştirilmesini istemiyoruz.
public record ProductAddedEvent(int Id, string Name, decimal Price) : IEvent;

// // ProductAddedEvent açık hali bu şekilde
// public record ProductAddedEvent2
// {
//     public int Id { get; init; } 
//     
//     public string Name { get; init; }
//     
//     public decimal Price { get; init; }
//
//     public ProductAddedEvent2(int Id, string Name, decimal Price)
//     {
//         this.Id = Id;
//         this.Name = Name;
//         this.Price = Price;
//     }
// };
