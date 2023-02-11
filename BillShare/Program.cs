using BillShare.Constants;
using BillShare.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureControllers();
builder.Services.ConfigureCors();
builder.Services.ConfigureAuthenticationAndAuthorization(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(CorsProfiles.AllowsAll);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();