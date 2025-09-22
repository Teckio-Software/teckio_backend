using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using ERP_TECKIO.Tenancy;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// --- Tenancy ---
builder.Services.AddScoped<ITenantResolver, RouteNumberTenantResolver>();
builder.Services.AddSingleton<IConnectionStringProvider, ConfigConnectionStringProvider>();

// Opciones base sin cadena (objeto “vacío” para partir de ahí)
builder.Services.AddSingleton(new DbContextOptions<AppDbContext>());

// Factory que crea DbContext por tenant
builder.Services.AddScoped<ITenantDbContextFactory, TenantDbContextFactory>();

// Inyección directa de AppDbContext (scoped) ya resuelto por request
builder.Services.AddScoped<AppDbContext>(sp =>
{
    var http = sp.GetRequiredService<IHttpContextAccessor>();
    var tenantCtx = (TenantContext?)http.HttpContext?.Items[nameof(TenantContext)];
    if (tenantCtx is null) throw new InvalidOperationException("Tenant no resuelto.");

    var factory = sp.GetRequiredService<ITenantDbContextFactory>();
    return factory.CreateDbContext(tenantCtx.TenantId);
});

// Add services to the container.
builder.Services.AddSingleton(provIder =>
    new MapperConfiguration(config =>
    {
        config.AddProfile(new AutoMapperProfile());
    }).CreateMapper());
var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");
builder.Services.AddCors(zOptions =>
{
    //var zvFrontEndUrl = builder.Configuration.GetValue<string>("FrontEnd_Url");
    zOptions.AddDefaultPolicy(zBuilder =>
    {
        zBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
          .WithExposedHeaders(new string[] { "CantidadTotalRegistros" });
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(zOpciones =>
    zOpciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection("llavejwt").Value!)),
        ClockSkew = TimeSpan.Zero
    });

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InyectarDependencias(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Debe ir ANTES de MapControllers
app.UseMiddleware<TenantMiddleware>();

app.MapControllers();

app.Run();
