namespace OrderManagementSystem.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        private readonly IEmailService _emailService;

        public OrderService(IUnitOfWork unitOfWork, IPaymentService paymentService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            _emailService = emailService;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var customerRepo = _unitOfWork.GetRepository<Domain.Entities.Customer, int>();
            var customer = await customerRepo.GetByIdAsync(dto.CustomerId) ?? throw new CustomerNotFoundException(dto.CustomerId);

            var productRepo = _unitOfWork.GetRepository<Domain.Entities.Product, int>();
            var productIds = dto.Items.Select(i => i.ProductId).ToList();
            var products = (await productRepo.GetAllAsync(new LoadProductsSpecification(productIds))).ToList();

            if (products.Count != productIds.Count)
                throw new Exception("One or more products not found");

            var orderItems = new List<OrderItem>();
            decimal total = 0;

            foreach (var item in dto.Items)
            {
                var product = products.First(p => p.Id == item.ProductId);

                if (item.Quantity > product.Stock)
                    throw new Exception($"Insufficient stock for product {product.Name}");

                decimal subtotal = product.Price * item.Quantity;
                total += subtotal;

                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    Discount = 0
                });
            }

            decimal discountPercentage = total > 200 ? 0.10m : total > 100 ? 0.05m : 0;
            decimal discountAmount = total * discountPercentage;
            decimal finalTotal = total - discountAmount;

            foreach (var item in orderItems)
            {
                var itemTotal = item.UnitPrice * item.Quantity;
                item.Discount = itemTotal / total * discountAmount;
            }

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = finalTotal,
                Status = OrderStatus.Pending,
                PaymentMethod = dto.PaymentMethod,
                OrderItems = orderItems
            };

            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            await orderRepo.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            //Step: Create Payment URL
            var paymentUrl = await _paymentService.CreatePaymentSession(order.Id);

            return new OrderResponseDto
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                PaymentUrl = paymentUrl,
                Items = [.. orderItems.Select(i => new OrderItemDetailDto
                {
                    ProductName = products.First(p => p.Id == i.ProductId).Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount
                })]
            };
        }


        public async Task<OrderResponseDto> GetOrderByIdAsync(int orderId)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var order = (await orderRepo.GetAsync(new GetOrderByIdSepcification(orderId)) ?? throw new OrderNotFoundException(orderId)) ?? throw new Exception("Order not found");
            return new OrderResponseDto
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                Items = order.OrderItems.Select(i => new OrderItemDetailDto
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount
                }).ToList()
            };
        }

        public async Task<List<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var orders = await orderRepo.GetAllAsync(new GetAllOrdersSepcification());

            return orders.Select(order => new OrderResponseDto
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                Items = order.OrderItems.Select(i => new OrderItemDetailDto
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount
                }).ToList()
            }).ToList();
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var order = (await orderRepo.GetAsync(new GetOrderByIdSepcification(orderId))
                ?? throw new OrderNotFoundException(orderId)) ?? throw new Exception("Order not found"); ;

            var customerRepo = _unitOfWork.GetRepository<Domain.Entities.Customer, int>();
            var customer = await customerRepo.GetAsync(new GetCustomerByIdSpecification(order.CustomerId))
                ?? throw new CustomerNotFoundException(order.CustomerId);

            order.Status = newStatus;
            await _emailService.SendEmailAsync(customer.User.Email!, "Order Status Updated", $"Your order status changed to {newStatus}");

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
