using MassTransit;
using PlaylistService.Consumers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<UserCreatedConsumer>();
    options.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["localhost"] ?? "beatify-rabbitmq-1", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        //cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(true));
        cfg.ConfigureEndpoints(context);

        /*cfg.ReceiveEndpoint("users", e =>
        {
            e.Consumer<UserCreatedConsumer>();
            //e.ConfigureConsumer<CommandMessageConsumer>(registrationContext);
            //e.PrefetchCount = 16;
            //e.UseMessageRetry(r => r.Interval(2, 100));
            //e.ConfigureConsumer<CommandMessageConsumer>(registrationContext);
        });*/
    });

});

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
