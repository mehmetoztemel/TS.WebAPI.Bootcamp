using ECommerce.WebAPI.Context;
using ECommerce.WebAPI.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "NLayer.API", Version = "v1" });
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "JWT Authorization header using the Bearer scheme. Put JWT Bearer token!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
      {
        new OpenApiSecurityScheme
         {
           Reference = jwtSecuritySheme.Reference,
         },
         new string[] {}
      }
     });
});
#endregion

#region JwtOptions
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        // Olu�turulacak token de�erini kimin da��tt���n� ifade edece�imiz aland�r. �rne�in; �www.myapi.com�
        //ValidIssuer = "",
        ValidateIssuer = false,
        // Olu�turulacak token de�erini hangi sitelerin kullanaca��n� belirledi�imiz aland�r.�rne�in; �www.xyz.com�
        //ValidAudience = "",
        ValidateAudience = false,
        // Olu�turulan token de�erinin s�resini kontrol edecek olan do�rulamad�r. E�er false olarak ayarlan�rsa �retilen token sonsuza dek kullan�l�l�r.
        ValidateLifetime = true,
        // �retilecek token de�erinin expire s�resinin belirtildi�i de�er kadar uzat�lmas�n� sa�layan �zelliktir.
        ClockSkew = TimeSpan.Zero,

        // �retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden security key verisinin do�rulanmas�d�r.
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenSettings:SecretKey").Value))
    };
});
#endregion

#region CORS Settings
builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(options =>
{
    options.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
}));

#endregion




builder.Services.AddDbContext<ECommerceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
