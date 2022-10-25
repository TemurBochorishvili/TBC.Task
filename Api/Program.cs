using Api.Core;
using Api.Mapping;
using Api.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPhysicalPersonRepository, PhysicalPersonRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(mc =>
{
    mc.AddProfile(new MappingProfile());
});

builder.Services.AddControllers();

builder.Services.AddDbContext<TaskDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

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

app.MapControllers();

app.UseRouting();

app.Run();


// TODO Remove
//{
//    "name": "Demur",
//    "lastName": "Dolenjishvili",
//    "gender": 1,
//    "personalNumber": "12312312321",
//    "dateOfBirth": "1997-01-01",
//    "cityId": 1,
//    "phoneNumbers": [
//        {
//        "number": "599 68 98 62",
//            "type": 1
//        }
//    ],
//    "pictureRelativePath": "c://"
//}