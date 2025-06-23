using AdminManager.Infrastructure.Contexts;
using AdminManager.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;

using Notifier.Shared.Extensions;
using Notifier.Shared.Interfaces;
using Notifier.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtBearerAuthentication(builder.Configuration);
builder.Services.AddScoped<IUserIdentifier, HttpUserIdentifier>();
builder.Services.AddAdminManagerModule(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var dbcontext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AdminDbContext>();
Console.WriteLine(dbcontext.Admins.Count());
// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();

