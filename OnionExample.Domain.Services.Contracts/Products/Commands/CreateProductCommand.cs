using MediatR;

namespace OnionExample.Domain.Services.Contracts.Products.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public CreateProductCommand(string title, string description, double price)
        {
            this.Title = title;
            this.Description = description;
            this.Price = price;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public double Price { get; private set; }
    }
}
