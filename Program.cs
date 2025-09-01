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
builder.Services.AddDbContext<Alumno03Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno03"));
});
builder.Services.AddDbContext<Alumno04Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno04"));
});
builder.Services.AddDbContext<Alumno05Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno05"));
});
builder.Services.AddDbContext<Alumno06Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno06"));
});
builder.Services.AddDbContext<Alumno07Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno07"));
});
builder.Services.AddDbContext<Alumno08Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno08"));
});
builder.Services.AddDbContext<Alumno09Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno09"));
});
builder.Services.AddDbContext<Alumno10Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno10"));
});
builder.Services.AddDbContext<Alumno11Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno11"));
});
builder.Services.AddDbContext<Alumno12Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno12"));
});
builder.Services.AddDbContext<Alumno13Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno13"));
});
builder.Services.AddDbContext<Alumno14Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno14"));
});
builder.Services.AddDbContext<Alumno15Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno15"));
});
builder.Services.AddDbContext<Alumno16Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno16"));
});
builder.Services.AddDbContext<Alumno17Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno17"));
});
builder.Services.AddDbContext<Alumno18Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno18"));
});
builder.Services.AddDbContext<Alumno19Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno19"));
});
builder.Services.AddDbContext<Alumno20Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno20"));
});
builder.Services.AddDbContext<Alumno21Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno21"));
});
builder.Services.AddDbContext<DemoTeckioContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoTeckio"));
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
