using System;
using System.Linq;
using StarterKit.Commands;

namespace StarterKit.Samples
{
    [TransactedCommand]
    public class CreateProductCommand : ICommand
    {
        public Guid ProductId { get; set; }
        public String ProductName { get; set; }
        public String ProductDescription { get; set; }
    }


    public class CreateProductCommandValidator : ICommandValidator<CreateProductCommand>
    {
        private readonly Database database;

        public CreateProductCommandValidator(Database database)
        {
            this.database = database;
        }

        public ErrorList IsValid(CreateProductCommand command)
        {
            var errors = new ErrorList();

            var existingProduct = database.Products.FirstOrDefault(p => p.Name == command.ProductName);
            if (existingProduct != null)
            {
                errors.Add("ProductName", "Unable to add this product. Record with this name already exists. Please choose another name");
            }

            return errors;
        }
    }

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly Database database;

        public CreateProductCommandHandler(Database database)
        {
            this.database = database;
        }

        public void Handle(CreateProductCommand command)
        {
            var product = new Product()
            {
                ProductId = command.ProductId,
                Name = command.ProductName,
                Description = command.ProductDescription,
            };

            database.Products.Add(product);
        }
    }
}
