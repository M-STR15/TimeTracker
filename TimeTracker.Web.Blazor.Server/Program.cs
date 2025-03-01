using Microsoft.OpenApi.Models;
using System.Reflection;
using TimeTracker.BE.Web.Shared.Infrastructure;
using TimeTracker.Web.Blazor.Server.Components;
using TimeTracker.Web.Blazor.Server.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var connectionString = "server=DESKTOP-JS0N1LD\\\\SQLEXPRESS; database=TimeTracker;Trusted_Connection=True;TrustServerCertificate=True;";
//builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddControllers();
builder.Services.AddTimeTrackerBeWebSharedService();

//builder.Services.AddDbContext<MsSqlDbContext>(options => options.UseSqlServer(connectionString)
//			.EnableSensitiveDataLogging()
//			.LogTo(Console.WriteLine), ServiceLifetime.Singleton);
if (connectionString != null)
	builder.Services.AddTimerTrackerBeWebSharedBusinessLogic(connectionString);


builder.Services.AddHostedService<ApplicationLifecycleLogger>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

	var beXmlPath = Path.Combine(AppContext.BaseDirectory, "TimerTracker.BE.Web.BusinessLogic.xml");

	options.IncludeXmlComments(xmlPath);
	options.IncludeXmlComments(beXmlPath);

	options.EnableAnnotations();
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "TimerTracker",
		Version = "v1",
		Description = ""
	});
});

var app = builder.Build();

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
