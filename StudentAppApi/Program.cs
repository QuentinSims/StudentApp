using Microsoft.EntityFrameworkCore;
using Student.Shared.DataLayer;
using StudentAppApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureJwtAuthenticationClaimsService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase(databaseName: "DemoDatabase"));

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseInMemoryDatabase(databaseName: "DemoDatabase"));

#region CORS
builder.ConfigureCORS();
#endregion

#region Authentication & Authorization
builder.ConfigureAuthenticationAndAuthorization();
#endregion

#region services
DatabaseConfiguration.ConfigureServices(builder.Services);
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePathBase("/api");
app.ConfigureCustomExceptionMiddleware();
app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

#region CORS
app.UseRouting();
app.UseCors(CorsConfiguration.PolicyName);
#endregion

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
