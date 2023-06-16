using FCoin;
using FCoin.Business;
using FCoin.Business.Interfaces;
using FCoin.Configuration;
using FCoin.Repositories;
using FCoin.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<FDbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<IClientManagement, ClientManagement>();
//builder.Services.AddScoped<IClientRepository, ClientRepository>();
//builder.Services.AddScoped<IHourManagement, HourManagement>();
//builder.Services.AddScoped<ISelectorManagement, SelectorManagement>();
//builder.Services.AddScoped<IValidatorManagement, ValidatorManagement>();
//builder.Services.AddScoped<IValidatorRepository, ValidatorRepository>();
//builder.Services.AddScoped<ITransactionManagement, TransactionManagement>();
//builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

AddServiceScopes.RegisterDependencies(builder.Services);

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

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

