using MetalHive.Data.DataModel;
using MetalHive.Data.DataServices.Interfaces;
using MetalHive.Data.DataServices;
using Microsoft.EntityFrameworkCore;
using MetalHive.Middleware;
using MetalHive;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerParameters>();

});


var connectionString = builder.Configuration.GetConnectionString("AzureDB");
builder.Services.AddDbContext<MetalHiveDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<IContractDataService, ContractDataService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ApiKeyMiddleware>();

app.Run();
