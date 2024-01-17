var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddHealthChecks();


// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/");

app.Run();
