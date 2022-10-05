using CleanGame.API;
using CleanGame.Application;
using CleanGame.Domain;
using CleanGame.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDomainServices()
    .AddApplicationServices()
    .AddInfraServices(builder.Configuration)
    .AddUIServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.AddInfraApplication();
app.AddUIApplication();

app.Run();