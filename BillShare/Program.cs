using BillShare.Constants;
using BillShare.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureCustomServices();
builder.Services.ConfigureControllers();
builder.Services.ConfigureMapper();
builder.Services.ConfigureCors();
builder.Services.ConfigureAuthenticationAndAuthorization(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

if(true && true){
    
}

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(CorsProfiles.AllowsAll);
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomExceptionHandler();
app.MapControllers();
app.Run();