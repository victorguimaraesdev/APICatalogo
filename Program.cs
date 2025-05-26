using Microsoft.OpenApi.Models; // Importação necessária para Swagger

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Adiciona Swagger (documentação da API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "APICatalogo#",
        Version = "v1"
    });
});

var app = builder.Build();

// Ativa o Swagger somente em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "APICatalogo");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapeia os controllers
app.MapControllers();

app.Run();
