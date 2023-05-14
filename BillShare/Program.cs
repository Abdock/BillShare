using BillShare.Constants;
using BillShare.Extensions;

var builder = WebApplication.CreateBuilder(args);
// builder.WebHost.UseUrls("http://192.168.65.178:5135", "http://localhost:5135");
// builder.WebHost.UseIISIntegration(); 
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureCustomServices();
builder.Services.ConfigureControllers();
builder.Services.ConfigureMapper();
builder.Services.ConfigureCors();
builder.Services.ConfigureAuthenticationAndAuthorization(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();
if (true || false)
{
    
}
app.UseSwagger();
app.UseSwaggerUI();   


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(CorsProfiles.AllowsAll);
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomExceptionHandler();
app.MapControllers();
app.Run();