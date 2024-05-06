using HealthChecks.UI.Client;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(action =>
{
    action.AddDefaultPolicy(options =>
    {
        options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });

    //action.AddPolicy("PolicyFirst", policy =>
    //{
    //    policy.WithMethods("GET", "POST", "PUT", "DELETE")
    //    .WithHeaders("Authorization")
    //    .WithOrigins("https://www.tspersonel.com");
    //});
});

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("limiter", configure =>
    {
        configure.PermitLimit = 100;
        configure.Window = TimeSpan.FromSeconds(1); // 1 Saniyede maksimum 100 istek kabul et
        configure.QueueLimit = 100; // 1 saniyede 100 den fazla istek gelirse bunlarýn ilk 100 tanesini kuyruða ekle
        configure.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; // Kuyruktaki istekleri eskiden yeniye doðru sýrala
    });
});

builder.Services.AddHealthChecks().AddCheck("apiInformation", () => HealthCheckResult.Healthy());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCors();
app.UseHealthChecks("/healthcheck", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.UseAuthorization();

app.MapControllers().RequireRateLimiting("limiter"); // bütün endpointlerde uygulamak için bu parametreyi aktif ediyoruz.

app.Run();