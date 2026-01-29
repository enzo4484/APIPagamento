var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// HttpClient to call Pagar.me API
builder.Services.AddHttpClient("pagarme", client =>
{
    var baseUrl = builder.Configuration["PagarMe:BaseUrl"] ?? "https://api.pagar.me/core/v5";
    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});
// register application and infrastructure services
builder.Services.AddScoped<Pagemento.Application.Services.IPaymentProvider, Pagamento.Infrastructure.Services.PagarMePaymentService>();
builder.Services.AddScoped<Pagemento.Application.Services.IPaymentService, Pagemento.Application.Services.PaymentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
