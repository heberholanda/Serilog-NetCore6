using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configura o Serilog como logger principal da aplicação
// CreateBootstrapLogger cria um logger inicial para capturar logs durante a inicialização
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

// Integra o Serilog com o host do ASP.NET Core
// ReadFrom.Configuration permite configurar o Serilog através do appsettings.json
builder.Host.UseSerilog((context, logConfiguration) => logConfiguration
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));

Log.Information("Starting up");

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
