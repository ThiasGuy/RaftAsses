using RaftAssesment;
using RaftAssesment.Interface;
using RaftAssesment.Services;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddHttpClient("ReqresClient", client =>
{
    client.BaseAddress = new Uri("https://reqres.in/");
    client.DefaultRequestHeaders.Add("x-api-key", "reqres-free-v1");
})
.AddPolicyHandler(GetRetryPolicy())
.AddPolicyHandler(GetTimeoutPolicy());



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Polly policies
IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
    HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2));

IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy() =>
    Policy.TimeoutAsync<HttpResponseMessage>(3); // Timeout after 3 seconds
