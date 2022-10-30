using Api.Core;
using Api.Mapping;
using Api.Middlewares;
using Api.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPhysicalPersonRepository, PhysicalPersonRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(mc =>
{
    mc.AddProfile(new MappingProfile());
});

builder.Services.AddMvc();
builder.Services.AddControllers();

builder.Services.AddDbContext<TaskDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

RequestLocalizationOptions requestLocalizationOptions = new()
{
    DefaultRequestCulture = new("en-US"),
    SupportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("ka-GE") },
    SupportedUICultures = new[] { new CultureInfo("en-US"), new CultureInfo("ka-GE") }
};

app.UseRequestLocalization(requestLocalizationOptions);

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.UseRouting();

app.Run();
