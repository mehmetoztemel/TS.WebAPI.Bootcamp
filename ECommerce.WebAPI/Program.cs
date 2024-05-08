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
        // Oluþturulacak token deðerini kimin daðýttýðýný ifade edeceðimiz alandýr. Örneðin; “www.myapi.com”
        //ValidIssuer = "",
        ValidateIssuer = false,
        // Oluþturulacak token deðerini hangi sitelerin kullanacaðýný belirlediðimiz alandýr.Örneðin; “www.xyz.com”
        //ValidAudience = "",
        ValidateAudience = false,
        // Oluþturulan token deðerinin süresini kontrol edecek olan doðrulamadýr. Eðer false olarak ayarlanýrsa üretilen token sonsuza dek kullanýlýlýr.
        ValidateLifetime = true,
        // Üretilecek token deðerinin expire süresinin belirtildiði deðer kadar uzatýlmasýný saðlayan özelliktir.
        ClockSkew = TimeSpan.Zero,

        // Üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden security key verisinin doðrulanmasýdýr.
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
