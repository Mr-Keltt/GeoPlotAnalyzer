using GeoPlotAnalyzer.API.Configuration;
using GeoPlotAnalyzer.API;
using GeoPlotAnalyzer.Services.Settings;

var builder = WebApplication.CreateBuilder(args);

var mainSettings = GeoPlotAnalyzer.Common.Settings.Settings.Load<MainSettings>("Main");
var logSettings = GeoPlotAnalyzer.Common.Settings.Settings.Load<LogSettings>("Log");
var swaggerSettings = GeoPlotAnalyzer.Common.Settings.Settings.Load<SwaggerSettings>("Swagger");

builder.Services.AddControllers();

builder.AddAppLogger(mainSettings, logSettings);



var services = builder.Services;

services.AddAppController();

services.AddHttpContextAccessor();

//services.AddAppDbContext();

services.AddAppHealthChecks();

services.AddAppSwagger(swaggerSettings);

services.RegisterServicesAndModels();

services.AddAppCors();


var app = builder.Build();

app.UseAppCors();

app.UseHttpsRedirection();

app.UseAppController();
app.MapControllers();

app.UseAppHealthChecks();

app.UseAppSwagger();

/*DbInitializer.Execute(app.Services);

DbSeeder.Execute(app.Services);*/

app.Run();

