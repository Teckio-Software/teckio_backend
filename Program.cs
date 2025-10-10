using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
//builder.Services.AddDbContext<Alumno01Context>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno01"));
//});
builder.Services.AddDbContext<DemoTeckioContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoTeckio"));
});
builder.Services.AddDbContext<IyATolucaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IyAToluca"));
});
builder.Services.AddDbContext<GrupoTeckioContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GrupoTeckio"));
});
builder.Services.AddDbContext<DemoTeckioAL04Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno04"));
});
builder.Services.AddDbContext<DemoTeckioAL05Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno05"));
});
builder.Services.AddDbContext<DemoTeckioAL06Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno06"));
});
builder.Services.AddDbContext<DemoTeckioAL07Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno07"));
});
builder.Services.AddDbContext<DemoTeckioAL08Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno08"));
});
builder.Services.AddDbContext<DemoTeckioAL09Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno09"));
});
builder.Services.AddDbContext<DemoTeckioAL10Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno10"));
});
builder.Services.AddDbContext<DemoTeckioAL11Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno11"));
});
builder.Services.AddDbContext<DemoTeckioAL12Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno12"));
});
builder.Services.AddDbContext<DemoTeckioAL13Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno13"));
});
builder.Services.AddDbContext<DemoTeckioAL14Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno14"));
});
builder.Services.AddDbContext<DemoTeckioAL15Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno15"));
});
builder.Services.AddDbContext<DemoTeckioAL16Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno16"));
});
builder.Services.AddDbContext<DemoTeckioAL17Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno17"));
});
builder.Services.AddDbContext<DemoTeckioAL18Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno18"));
});
builder.Services.AddDbContext<DemoTeckioAL19Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno19"));
});
builder.Services.AddDbContext<DemoTeckioAL20Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno20"));
});
builder.Services.AddDbContext<DemoTeckioAL21Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno21"));
});
builder.Services.AddDbContext<DemoTeckioAL22Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno22"));
});
builder.Services.AddDbContext<DemoTeckioAL23Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno23"));
});
builder.Services.AddDbContext<DemoTeckioAL24Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno24"));
});
builder.Services.AddDbContext<DemoTeckioAL25Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno25"));
});
builder.Services.AddDbContext<DemoTeckioAL26Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno26"));
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
