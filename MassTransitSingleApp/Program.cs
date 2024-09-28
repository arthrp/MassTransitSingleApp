using MassTransit;

namespace MassTransitSingleApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMassTransit(bc =>
        {
            bc.AddConsumer<TimeMessageConsumer>();
            bc.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        builder.Services.AddHostedService<TimeMessageProducer>();
        builder.Services.AddSingleton<MessageProvider>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("/lastmessage", (MessageProvider msgProvider) =>
        {
            var msg = msgProvider.GetLatest();

            return msg;
        });
        

        app.Run();
    }
}