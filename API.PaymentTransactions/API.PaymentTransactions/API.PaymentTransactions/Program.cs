using API.FurnitureStore.API.Services;
using API.PaymentTransactions.API.Configuration;
using API.PaymentTransactions.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Furniture_Store_API",
            Version = "v1"
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = $@"JWT Authorization header using the Bearer scheme. 
                        \r\n\r\n Enter prefix (Bearer), space, and then your token. 
                        Example: 'Bearer 1231233kjsdlkajdksad'"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
            Reference = new OpenApiReference{
                Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string [] { }
        }
    });
    });

    builder.Services.AddDbContext<APIPaymentTransactionsContext>(options =>
        options.UseSqlServer(builder.Configuration["ConnectionStrings:APIPaymentTransactionsContext"]));

    builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JwtConfig"));

    //Email
    builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
    builder.Services.AddSingleton<IEmailSender, EmailService>();

    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("jwtConfig:Secret").Value);
    var tokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };

    builder.Services.AddSingleton(tokenValidationParameters);


    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddJwtBearer(jwt =>
    {
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = tokenValidationParameters;
    });

    builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<APIPaymentTransactionsContext>();


    // nLOG
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();


}
catch (Exception ex)
{
    logger.Error(ex, "There has been an error");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
