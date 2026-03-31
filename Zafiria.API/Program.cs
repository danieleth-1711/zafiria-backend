using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Zafiria.Infrastructure.Data;
using Zafiria.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// DbContext con PostgreSQL
builder.Services.AddDbContext<ZafiriaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .ConfigureWarnings(w => w.Ignore(
        Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

// Keycloak JWT
var keycloak = builder.Configuration.GetSection("Keycloak");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = keycloak["Authority"];
    options.Audience = keycloak["Audience"];
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new()
    {
        ValidateAudience = false,
        ValidateIssuer = true,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.AddControllers();
// FluentValidation
builder.Services.AddScoped<CrearJoyaDtoValidator>();
builder.Services.AddScoped<CrearReservaDtoValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Aplicar migraciones automáticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ZafiriaDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();