using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

string ConnectionString = builder.Configuration.GetConnectionString("MySQL");
// Injection de dépendance = même dépendance pour toutes les class qui l'injecte 
builder.Services.AddSingleton(new DefaultSqlConnectionFactory(ConnectionString));

// Injection de dépendance = dépendance différents pour chaque class qui injecte
builder.Services.AddScoped<IPostsRepository, PostsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();

//Ajoute le service pour le Swagger
builder.Services.AddSwaggerGen();
// ... pour le cors
builder.Services.AddCors();

// Configure la migration 
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(config => config
        .AddMySql5()
        .WithGlobalConnectionString(ConnectionString)
        .ScanIn(Assembly.GetExecutingAssembly()).For.All())
    .AddLogging(lb => lb.AddFluentMigratorConsole()
    );

// Configure le middleware pour le token JWT
string JwtKey = builder.Configuration["Jwt"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Lance la migration
using var scope = app.Services.CreateScope();
var migrator = scope.ServiceProvider.GetService<IMigrationRunner>()!;
migrator.MigrateUp();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Permet que le cors te fasse pas chier (autorise n'importe quoi)
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
