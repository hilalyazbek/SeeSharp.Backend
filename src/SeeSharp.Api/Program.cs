using SeeSharp.GraphQL;
using SeeSharp.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApiServices(builder.Configuration)
    .AddGraphQLServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);


//builder.Host.UseSerilog((context, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

// Allow httprequest logs in Serilog
//app.UseSerilogRequestLogging();

app.UseApiVersioning();

app.MapGraphQL();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

