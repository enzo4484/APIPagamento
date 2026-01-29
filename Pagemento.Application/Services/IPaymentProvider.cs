using System.Threading.Tasks;
using Pagamento.Domain.Pedido;
using Pagamento.Domain;

namespace Pagemento.Application.Services
{
    public interface IPaymentProvider
    {
        Task<PaymentResponse> CreateAsync(Pedido pedido);
    }
}
