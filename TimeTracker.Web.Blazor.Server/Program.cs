using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TimeTracker.Web.Blazor.Server.Components;
using TimeTracker.Web.Blazor.Server.Helpers;
using TimeTracker.BE.Web.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("TimeTracker");

//builder.Services.AddDbContextFactory<ToDoDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

builder.Services.AddControllers();
builder.Services.AddTimeTrackerBeWebSharedService();
builder.Services.AddTimerTrackerBeWebSharedBusinessLogicService();
//builder.Services.AddToDoListBeBusinessLogicService();

//builder.Services.AddScoped<ToDoRepository>();

//builder.Services.AddScoped<ToDoController>();

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
