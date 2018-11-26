using MediatR;

namespace OnionExample.Domain.Services.Contracts.Products.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public UpdateProductCommand(int id, string title, string description, double price)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Price = price;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public double Price { get; private set; }
    }
}
