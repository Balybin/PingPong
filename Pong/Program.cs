using Microsoft.EntityFrameworkCore;
using Pong;
using Pong.Services;

using NLog;
using NLog.Web;


var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{

    var builder = WebApplication.CreateBuilder(args);

    // Logging
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Configure DB
    builder.Services.AddDbContext<AppDbContext>(builder =>
    {
        builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=messagedb;Trusted_Connection=True;");
    });

    //DI
    builder.Services.AddScoped<IMessageRepository, MessageRepository>();
    builder.Services.AddScoped<IMessageService, MessageService>();

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
}
catch (Exception e)
{
    logger.Error(e, "Stopped because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
