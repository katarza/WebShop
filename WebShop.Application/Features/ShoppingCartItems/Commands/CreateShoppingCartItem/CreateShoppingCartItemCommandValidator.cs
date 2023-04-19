using FluentValidation;
using WebShop.Application.Contracts.Persistence;
using WebShop.Domain.Entities;

namespace WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem
{
    internal class CreateShoppingCartItemCommandValidator : AbstractValidator<CreateShoppingCartItemCommand>
    {

        private readonly IProductRepository _productRepository;
        public CreateShoppingCartItemCommandValidator(IProductRepository productRepository)
        {

            _productRepository = productRepository;

            RuleFor(p => p.CustomerId)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.ProductId)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required")
            .MustAsync(ProductIdExists).WithMessage("Non existent product with given product ID");

            RuleFor(p => p.Quantity)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThan(0).WithMessage("{PropertyName} must be positive");

            RuleFor(p => new { p.Quantity, p.ProductId })
            .MustAsync((x, cancellation) => RequiredQuantityAvailable(x.ProductId, x.Quantity)).WithMessage("Required quantity is not available"); ;
        }

        private async Task<bool> RequiredQuantityAvailable(Guid productId, int quantity)
        {
            return (await _productRepository.IsRequiredProductQuantityAvailable(productId, quantity));
        }

        private async Task<bool> ProductIdExists(Guid productId, CancellationToken token)
        {
            return (await _productRepository.DoesProductIdExist(productId));
        }
    }
}
