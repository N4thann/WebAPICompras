using APICompras.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APICompras", Version = "v1" });
    //Esse codigo foi adicionado para resolver ações conflitantes que é quando ações estão
    //usando a mesma rota
    c.ResolveConflictingActions(x => x.First());
});

builder.Services.AddDbContext<Contexto>
    (options => options.UseSqlServer("Data Source=DESKTOP-JTHDJFE\\SQLSERVER;Initial Catalog=SistemaCompras;Integrated Security=True;TrustServerCertificate=True"));


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
