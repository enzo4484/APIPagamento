using System;
using System.Collections.Generic;
using System.Text;

namespace Pagamento.Domain.Pedido
{
    public class Pedido
    {
        public Guid Id { get; set; }

        public string Currency { get; set; }

        public string Status { get; set; }

        public string Code { get; set; }

        public object Customer { get; set; }

        public object Shipping { get; set; }

        public object Antifraud { get; set; }

        public List<object> Payments { get; set; } = new();

        public object Items { get; set; }

        public bool Closed { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public object Metadata { get; set; }

        public string RecurrenceCycle { get; set; }
    }
}
