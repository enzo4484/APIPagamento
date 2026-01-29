using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Pagamento.Domain.Pedido;
using Pagamento.Domain;
using Pagemento.Application.Services;
using Microsoft.Extensions.Configuration;

namespace Pagamento.Infrastructure.Services
{
    public class PagarMePaymentService : Pagemento.Application.Services.IPaymentProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public PagarMePaymentService(IHttpClientFactory httpClientFactory, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<PaymentResponse> CreateAsync(Pedido pedido)
        {
            if (pedido == null) throw new ArgumentNullException(nameof(pedido));

            var client = _httpClientFactory.CreateClient("pagarme");

            var apiKey = _configuration["PagarMe:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("PagarMe ApiKey not configured");

            var authScheme = _configuration["PagarMe:AuthScheme"] ?? "Bearer";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authScheme, apiKey);

            var endpoint = _configuration["PagarMe:PaymentEndpoint"] ?? "/transactions";

            var response = await client.PostAsJsonAsync(endpoint, pedido);
            var content = await response.Content.ReadAsStringAsync();

            return new PaymentResponse { Raw = content };
        }
    }
}
