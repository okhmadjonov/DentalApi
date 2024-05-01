using Dental.Api.Configurations;
using Dental.Api.Middlewares;
using Dental.Infrastructure.Context;
using Dental.Service.Extentions;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DentalDbContext>(option =>
{
    option.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o =>
        {
            o.MigrationsAssembly("Dental.Api");
        }
    );
    option.EnableSensitiveDataLogging();
});


builder.Services.AddServiceFunctionsConfiguration()
            .AddErrorFilter()
            .AddServiceConfiguration()
            .AddSwaggerService(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    using var db = scope.ServiceProvider.GetRequiredService<DentalDbContext>();
    // db.Database.Migrate();

}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseStaticFiles();
//app.UseHttpsRedirection();
//app.UseMiddleware<DentalExceptionMiddleware>();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
