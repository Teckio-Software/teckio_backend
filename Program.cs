using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<Alumno01Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno01"));
});
builder.Services.AddDbContext<Alumno02Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno02"));
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

app.MapControllers();

app.Run();
