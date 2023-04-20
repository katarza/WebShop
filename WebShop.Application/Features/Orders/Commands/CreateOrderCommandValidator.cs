using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Contracts.Persistence;

namespace WebShop.Application.Features.Orders.Commands
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandValidator(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

            RuleFor(p => p.CustomerId)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.CustomerPhone)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.ShippingAddress)
            .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.ShippingAddress.Street)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.ShippingAddress.HouseNumber)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.ShippingAddress.City)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required");
        }

    }
}
