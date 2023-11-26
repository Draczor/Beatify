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
    options.AddConsumer<MyMessageConsumer>();
    options.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("beatify-rabbitmq-1", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
        /*cfg.ReceiveEndpoint("UserCreated", e =>
        {
            e.ConfigureConsumer<UserCreatedConsumer>(context);
        });*/
    });
    options.AddConsumers(typeof(Program).Assembly);
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
