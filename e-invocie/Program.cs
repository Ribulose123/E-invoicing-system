using e_invocie.IServices;
using e_invocie.Services;
using E_invocing.Domin.Entities;
using E_invocing.Domin.InterFaces;
using E_invocing.Persistence;
using E_invoicing.Infrastructure.Logic.Tax___Fx;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TaxApiOptions>(
    builder.Configuration.GetSection("TaxApi"));

builder.Services.AddHttpClient<ITaxService, TaxService>();
builder.Services.AddScoped<IUploadbatch, UploadBatchServices>();

builder.Services.Configure<FxApiOptions>(
    builder.Configuration.GetSection("FxOptions"));
builder.Services.AddHttpClient<IFxServices, FxServices>();


// Dbconnection string
builder.Services.AddDbContext<E_invocingDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// To serialize enum as string in json response
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
