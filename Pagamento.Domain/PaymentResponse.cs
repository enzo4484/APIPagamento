using System.Text.Json.Serialization;

namespace Pagamento.Domain
{
    public class PaymentResponse
    {
        [JsonPropertyName("raw")]
        public string Raw { get; set; }
    }
}
