using System;
using System.Collections.Generic;
using System.Text;

namespace Pagamento.Domain.Items
{
    public class Items
    {
        public Guid Id { get; set; }

        public int Amount { get; set; }

        public string Description { get; set; }

        public short Quantity { get; set; }

        public string Code { get; set; }

        public string Category { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public object Order { get; set; }
    }
}
