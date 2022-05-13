using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using Service;
using Service.Filters;
using Service.Manager;
using System.Text.Json.Serialization;
using TringleCandidateProject.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//ENUM this is Convert Value
builder.Services.AddControllers(options =>
{
    //Burada Validation Filter Eklenerek T�m Do�rulamalar� tek tek �a��rmak yerine ServiceAssembly'de bulunan tamam�n� i�leme al�yor.
    options.Filters.Add(new ValidationFilter());
}).AddFluentValidation(
    x=>x.RegisterValidatorsFromAssemblyContaining<ServiceAssembly>()
    ).AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opts.JsonSerializerOptions.IgnoreNullValues = true;
});

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    //Hata Verirse Middleware'da Bulunan Global Exception'� Kullanarak Hata Mesajlar�n� �zelle�tirmemizi Sa�l�yor
    opt.SuppressModelStateInvalidFilter = true;
});

//DI CONTAINER --- AutoFac Ninject gibi di�er k�t�phanelerde kullan�labilir.

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountManager, AccountManager>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionManager, TransactionManager>();

//Action Filter Araya girerek Account Bulunamaz ise Hata Vermesini Sa�lar 
builder.Services.AddScoped<NotFoundAccountFilter>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Middleware Exception'lar� De�i�tirmemizi Sa�l�yor
app.UseGlobalExceptionMiddleware();
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
