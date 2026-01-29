using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pagamento.Domain.Pedido;
using Pagamento.Domain;

namespace Pagemento.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentProvider _provider;

        // in-memory store for business logic
        private static readonly ConcurrentDictionary<Guid, Pedido> _store = new();

        public PaymentService(IPaymentProvider provider)
        {
            _provider = provider;
        }

        public async Task<PaymentResponse> CreateAsync(Pedido pedido)
        {
            if (pedido == null) throw new ArgumentNullException(nameof(pedido));

            if (pedido.Id == Guid.Empty)
                pedido.Id = Guid.NewGuid();

            pedido.CreatedAt = DateTime.UtcNow;
            pedido.UpdatedAt = pedido.CreatedAt;

            var result = await _provider.CreateAsync(pedido);

            // business keeps a local record
            _store[pedido.Id] = pedido;

            return result;
        }

        public IEnumerable<Pedido> GetAll() => _store.Values;

        public Pedido? Get(Guid id)
        {
            _store.TryGetValue(id, out var pedido);
            return pedido;
        }

        public void Update(Guid id, Pedido pedido)
        {
            if (pedido == null) throw new ArgumentNullException(nameof(pedido));
            pedido.Id = id;
            pedido.UpdatedAt = DateTime.UtcNow;
            _store[id] = pedido;
        }

        public bool Delete(Guid id) => _store.TryRemove(id, out _);
    }
}
