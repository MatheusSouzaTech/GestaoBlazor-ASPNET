using APIGestão.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = null
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("WasmClient", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // Em desenvolvimento, permite qualquer origem (importante para testes em celular)
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }
        else
        {
            // Em produção, especifique os domínios permitidos
            policy.WithOrigins(
                    "https://localhost:7200",
                    "http://localhost:5200",
                    "https://localhost:7012",
                    "http://localhost:5047")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AppDbContext>(sp =>
    sp.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext());

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<TransportadoraService>();
builder.Services.AddScoped<MarcaService>();

// Novos serviços
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<FilialService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<UnidadeMedidaService>();
builder.Services.AddScoped<ArmazemService>();
builder.Services.AddScoped<TabelaPrecoService>();
builder.Services.AddScoped<CondicaoPagamentoService>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("WasmClient");

app.MapControllers();

app.Run();
