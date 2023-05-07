using FluentValidation;
using WebShop.Application.Contracts.Persistence;

namespace WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem
{
    public class CreateShoppingCartItemCommandValidator : AbstractValidator<CreateShoppingCartItemCommand>
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

        }

        private async Task<bool> ProductIdExists(Guid productId, CancellationToken token)
        {
            return (await _productRepository.DoesProductIdExist(productId));
        }
    }
}
