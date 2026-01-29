using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pagamento.Domain.Pedido;
using Pagamento.Domain;

namespace Pagemento.Application.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> CreateAsync(Pedido pedido);

        IEnumerable<Pedido> GetAll();

        Pedido? Get(Guid id);

        void Update(Guid id, Pedido pedido);

        bool Delete(Guid id);
    }
}
