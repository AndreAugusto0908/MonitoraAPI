using MonitoraAPI.Infra;
using MonitoraAPI.Service.EspService;
using Microsoft.EntityFrameworkCore;
using MonitoraAPI.Infra.Repository.Esp;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Adiciona comentários XML ao Swagger
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Configuração do banco de dados com MySQL
var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("defaultConnection"), serverVersion)
        .EnableSensitiveDataLogging() // Mostra parâmetros das queries no log (apenas para desenvolvimento)
        .LogTo(Console.WriteLine));

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configuração de dependências
builder.Services.AddScoped<IEspRepository, EspRepository>();
builder.Services.AddScoped<IEspService, EspService>();

var app = builder.Build();

// Ativa Swagger em todos os ambientes (ajuste conforme necessidade)
app.UseSwagger();
app.UseSwaggerUI();

// Middleware de redirecionamento HTTPS
app.UseHttpsRedirection();

// Middleware de CORS
app.UseCors("AllowAll");

// Middleware de autorização (se necessário)
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

// Inicia a aplicação
app.Run();