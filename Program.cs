using MassTransit;

namespace SqlimportProducer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Start of services declaration for masstransit rabbitmq
            {
                builder.Services.AddMassTransit(config =>
                {
                    config.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host("amqp://guest:guest@localhost:5672", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                    });
                });
            }
            //End of services declaration for masstransit rabbitmq


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
    }
}