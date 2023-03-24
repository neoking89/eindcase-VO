using Microsoft.EntityFrameworkCore;
using eindcaseAPI.DAL;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICourseRepository, CourseRepository>(); 
builder.Services.AddScoped<ICourseInstanceRepository, CourseInstanceRepository>(); 
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Build CORS Policy
builder.Services.AddCors(options => options.AddPolicy(name: "Courses",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add CORS Policy
app.UseCors("Courses");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();