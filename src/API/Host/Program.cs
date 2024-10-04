using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using UserManagement.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
                .WithMetrics(m =>
                {
                    m.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("user management"))
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("UserManagement.Users")
                    .AddAspNetCoreInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddOtlpExporter(ex =>
                    {
                        ex.Endpoint = new Uri("http://localhost:4317");
                        ex.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                    }); ;
                })
                .WithTracing(t =>
                {
                    t.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("user management"))
                    .AddAspNetCoreInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddOtlpExporter(ex =>
                    {
                        ex.Endpoint = new Uri("http://localhost:4317");
                        ex.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                    });
                })
                .WithLogging(l => l.AddOtlpExporter(ex =>
                {
                    ex.Endpoint = new Uri("http://localhost:4318/v1/logs");
                    ex.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                }), opt => opt.IncludeScopes = true);

builder.Services.AddControllers().AddApplicationPart(typeof(UserManagement.Web.DependencyInjectionExt).Assembly);
builder.Services.AddUserManagementServices(opt => { opt.UseConnectionString("server=localhost;Database=users;Integrated Security = true;Trust server certificate=true;"); });

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
