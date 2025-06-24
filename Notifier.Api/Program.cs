using AdminManager.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;

using Notifier.Shared.Extensions;
using Notifier.Shared.Interfaces;
using Notifier.Shared.Services;

using ProfileManager.Infrastructure.Contexts;

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

