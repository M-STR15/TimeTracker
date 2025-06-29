using Microsoft.OpenApi.Models;
using System.Reflection;
using TimeTracker.Web.Blazor.Server.Components;
using TimeTracker.Web.Blazor.Server.Helpers;
using TimeTracker.Web.Blazor.Server.Infrastructure;
using TimeTracker.FE.Web.Components.Infrastructure;
using TimeTracker.BE.Web.BusinessLogic.Infrastructure;
using TimeTracker.BE.DB.DataAccess;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();


builder.Services.AddControllers();
builder.Services.AddTimeTrackerWebBlazorServer();
builder.Services.AddTimeTrackerFeComponents();

if (builder.Environment.IsEnvironment("Testing"))
	builder.Services.AddTimeTrackerBeWebSharedBusinessLogic<MsSqlDbContext>();
else
	builder.Services.AddTimeTrackerBeWebSharedBusinessLogic<MsSqlDbContext>();


builder.Services.AddHostedService<ApplicationLifecycleLogger>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

	var beXmlPath = Path.Combine(AppContext.BaseDirectory, "TimeTracker.BE.Web.BusinessLogic.xml");

	options.IncludeXmlComments(xmlPath);
	options.IncludeXmlComments(beXmlPath);

	options.EnableAnnotations();
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "TimeTrackerAPI",
		Version = "v1",
		Description = ""
	});
});

builder.Services.AddServerSideBlazor()
	.AddCircuitOptions(options => { options.DetailedErrors = true; });

var apiBaseUrl = builder.Configuration["ProjectUrl"] ?? "https://localhost:5001";
builder.Services.AddHttpClient("TimeTrackerAPI", client =>
{
	client.BaseAddress = new Uri(apiBaseUrl);
});

var app = builder.Build();

app.UseStatusCodePagesWithRedirects("/Error");

app.UseRouting();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
	c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
});

app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();


public partial class Program { }