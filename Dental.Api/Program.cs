using Dental.Api.Configurations;
using Dental.Api.Middlewares;
using Dental.Service.Interfaces.Auth;
using Dental.Service.Interfaces.Students;
using Dental.Service.Interfaces.Tokens;
using Dental.Service.Interfaces.Users;
using Dental.Service.Services.Students;
using Dental.Service.Services.Users;
using Dental.Service.Services.Auth;
using Dental.Service.TokenGenerators;
using Microsoft.EntityFrameworkCore;
using Dental.Infrastructure.Context;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITokenRepository, TokenGenerator>();
builder.Services.AddScoped<IAuthRepository, AuthService>();
builder.Services.AddScoped<IUserRepository, UserService>();
builder.Services.AddScoped<IStudentRepository, StudentService>();



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
            .AddSwaggerService(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    using var db = scope.ServiceProvider.GetRequiredService<DentalDbContext>();
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
app.UseMiddleware<DentalExceptionMiddleware>();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
