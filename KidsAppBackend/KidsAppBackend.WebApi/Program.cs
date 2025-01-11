using KidsAppBackend.Data.Repositories;
using KidsAppBackend.Data.UnitOfWork;
using KidsAppBackend.Business.Operations.User;
using Microsoft.EntityFrameworkCore;
using KidsAppBackend.Data;

// Program.cs veya Startup.cs

var builder = WebApplication.CreateBuilder(args);

// DbContext kayıt
builder.Services.AddDbContext<KidsAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository pattern için açık generik kayıt
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// UnitOfWork kayıt
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// UserService kayıt
builder.Services.AddScoped<IUserService, UserManager>();

// Diğer servis kayıtları...
builder.Services.AddControllers();

// Swagger ve diğer middleware kayıtları...
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware Kullanımı
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
