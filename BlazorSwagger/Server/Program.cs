using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using blazorSwagger.Models;
using blazorSwagger.Services;
using MongoDB.Driver;
using System.Reflection;

// https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
// https://www.youtube.com/playlist?list=PL6n9fhu94yhVowClAs8-6nYnfsOTma14P (entire Blazor)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<EmployeeStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(EmployeeStoreDatabaseSettings)));

builder.Services.AddSingleton<IEmployeeStoreDatabaseSettings>(sp =>
sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<EmployeeStoreDatabaseSettings>>().Value);

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddSingleton<IMongoClient>(s =>
new MongoClient(builder.Configuration.GetValue<string>("EmployeeStoreDatabaseSettings:ConnectionString")));

builder.Services.AddIgniteUIBlazor();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee API",
        Description = "An ASP.NET Core Web API for managing employees and departments",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    options.OrderActionsBy(apiDesc => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
    // generate the xml docs that'll drive the swagger docs
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
