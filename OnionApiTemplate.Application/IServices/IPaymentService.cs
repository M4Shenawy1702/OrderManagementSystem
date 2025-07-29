namespace OrderManagementSystem.Application.IServices
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentSession(int observationId);
        Task<string> CheckoutSuccess(string sessionId, int observationId);
        Task<string> CheckoutFailed(string sessionId);
    }
}
