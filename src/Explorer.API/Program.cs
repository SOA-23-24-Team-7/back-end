using Explorer.API.Startup;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.BuildingBlocks.Infrastructure.HTTP;
using Explorer.Tours.Core.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
builder.Services.AddControllers();
builder.Services.AddHostedService<MailingListScheduler>();
builder.Services.ConfigureSwagger(builder.Configuration);
const string corsPolicy = "_corsPolicy";
builder.Services.ConfigureCors(corsPolicy);
builder.Services.ConfigureAuth();

builder.Services.RegisterModules();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors(corsPolicy);
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();

// Required for automated tests
namespace Explorer.API
{
    public partial class Program { }
}