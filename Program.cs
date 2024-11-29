using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using Microsoft.OpenApi.Models;

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("Alumno18"));
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
builder.Services.AddDbContext<Alumno22Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno22"));
});
builder.Services.AddDbContext<Alumno23Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno23"));
});
builder.Services.AddDbContext<Alumno24Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno24"));
});
builder.Services.AddDbContext<Alumno25Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno25"));
});
builder.Services.AddDbContext<Alumno26Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno26"));
});
builder.Services.AddDbContext<Alumno27Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno27"));
});
builder.Services.AddDbContext<Alumno28Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno28"));
});
builder.Services.AddDbContext<Alumno29Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno29"));
});
builder.Services.AddDbContext<Alumno30Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno30"));
});
builder.Services.AddDbContext<Alumno31Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno31"));
});
builder.Services.AddDbContext<Alumno32Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno32"));
});
builder.Services.AddDbContext<Alumno33Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno33"));
});
builder.Services.AddDbContext<Alumno34Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno34"));
});
builder.Services.AddDbContext<Alumno35Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno35"));
});
builder.Services.AddDbContext<Alumno36Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno36"));
});
builder.Services.AddDbContext<Alumno37Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("alumno37"));
});
builder.Services.AddDbContext<Alumno39Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Equipo1"));
});
builder.Services.AddDbContext<Alumno38Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Equipo6"));
});
builder.Services.AddDbContext<Alumno40Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Equipo2"));
});
builder.Services.AddDbContext<Alumno41Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Equipo3"));
});
builder.Services.AddDbContext<Alumno42Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Equipo4"));
});
builder.Services.AddDbContext<Alumno43Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Equipo5"));
});
builder.Services.AddDbContext<Alumno44Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Equipo7"));
});

builder.Services.InyectarDependencias(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
