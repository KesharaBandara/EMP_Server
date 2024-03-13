using emp_server.Contracts;
using emp_server.Data;
using emp_server.Repository;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProductsAPIDbContext>();
builder.Services.AddScoped<IEmpRepository,EmpRepository>();
//builder.Services.AddDbContext<ContactsAPIDbContext>(options => options.UseInMemoryDatabase("contactsDB"));
//builder.Services.AddDbContext<ProductsAPIDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("emp_server_connectionString")));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
var app = builder.Build();
//var cultureInfo = new CultureInfo("en-US");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
