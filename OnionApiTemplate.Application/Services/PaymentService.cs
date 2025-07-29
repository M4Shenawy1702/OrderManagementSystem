using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Stripe.Checkout;

namespace OrderManagementSystem.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServer _server;

        public PaymentService(IUnitOfWork unitOfWork, IServer server)
        {
            _unitOfWork = unitOfWork;
            _server = server;
        }

        public async Task<string> CreatePaymentSession(int orderId)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var order = await orderRepo.GetByIdAsync(orderId);
            if (order == null)
                throw new OrderNotFoundException(orderId);

            if (order.Status == OrderStatus.Paid)
                throw new Exception("The order has already been paid.");

            var baseUrl = _server.Features.Get<IServerAddressesFeature>()?.Addresses.FirstOrDefault();

            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("Failed to determine API base URL.");

            return await GeneratePaymentUrl(baseUrl, order.Id, order.TotalAmount);
        }

        public async Task<string> CheckoutSuccess(string sessionId, int orderId)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var order = await orderRepo.GetAsync(new GetOrderByIdSepcification(orderId));

            if (order == null)
                throw new OrderNotFoundException(orderId);

            if (order.Status == OrderStatus.Paid)
                throw new Exception("The order has already been paid.");

            var sessionService = new SessionService();
            var session = await sessionService.GetAsync(sessionId);

            var updateStock = await _unitOfWork.OrderRepository.UpdateStockAsync(orderId);

            if (!updateStock)
                throw new BadRequestException(["Failed to update stock."]);

            var invoice = new Invoice
            {
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = order.TotalAmount,
                OrderId = orderId
            };

            order.Status = OrderStatus.Paid;

            var invoiceRepo = _unitOfWork.GetRepository<Invoice, int>();
            await invoiceRepo.AddAsync(invoice);

            await _unitOfWork.SaveChangesAsync();

            return "Payment successful.";
        }


        public Task<string> CheckoutFailed(string sessionId)
        {
            return Task.FromResult("Payment canceled.");
        }

        private async Task<string> GeneratePaymentUrl(string baseUrl, int orderId, decimal totalAmount)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            UnitAmountDecimal = totalAmount * 100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Order #{orderId}",
                            },
                        },
                        Quantity = 1,
                    }
                },
                Mode = "payment",
                SuccessUrl = $"{baseUrl}/api/payments/success?sessionId={{CHECKOUT_SESSION_ID}}&orderId={orderId}",
                CancelUrl = $"{baseUrl}/api/payments/failed?sessionId={{CHECKOUT_SESSION_ID}}",
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }
    }
}
