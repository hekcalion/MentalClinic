using MentalClinic.API.Repositories;
using Microsoft.Azure.Cosmos;
namespace MentalClinic.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddSingleton<CosmosClient>(new CosmosClient(builder.Configuration.GetValue<string>("CosmosDataBase:ConnectionString")));
        builder.Services.AddSingleton<Database>(sp =>
        {
            var cosmosClient = sp.GetRequiredService<CosmosClient>();
            return cosmosClient.CreateDatabaseIfNotExistsAsync(builder.Configuration.GetValue<string>("CosmosDataBase:DatabaseName")).GetAwaiter().GetResult();
        });

        builder.Services.AddScoped<EmployeeRepository>(sp =>
        {
            var database = sp.GetRequiredService<Database>();
            database.CreateContainerIfNotExistsAsync("Employee", "/id").GetAwaiter().GetResult();
            return new EmployeeRepository(database.GetContainer("Employee"));
        });

        builder.Services.AddScoped<AppointmentRepository>(sp =>
        {
            var database = sp.GetRequiredService<Database>();
            database.CreateContainerIfNotExistsAsync("Appointment", "/id").GetAwaiter().GetResult();
            return new AppointmentRepository(database.GetContainer("Appointment"));
        });

        builder.Services.AddScoped<TestRepository>(sp =>
        {
            var database = sp.GetRequiredService<Database>();
            database.CreateContainerIfNotExistsAsync("Test", "/id").GetAwaiter().GetResult();
            return new TestRepository(database.GetContainer("Test"));
        });

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
    }
}
