using NLog;
using NLog.Web;
using Microsoft.AspNetCore.HttpLogging;
using LendFlow.Systems.API.Middleware;
using LendFlow.Systems.API.ServiceExtensions;
using LendFlow.Systems.BLL.Interfaces;
using LendFlow.Systems.BLL.Services;
using LendFlow.Systems.DAL.Interfaces;
using LendFlow.Systems.DAL.Repositories;
using Microsoft.Extensions.FileProviders;

// Initialize NLog early to catch startup errors
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // 1. Clear default logging providers and register NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // 2. Configure Global HTTP Request / Response tracking parameters
    builder.Services.AddHttpLogging(logging =>
    {
        logging.LoggingFields = HttpLoggingFields.RequestMethod |
                                HttpLoggingFields.RequestPath |
                                HttpLoggingFields.ResponseStatusCode;
    });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin", policy =>
        {
            policy.WithOrigins("https://localhost:7176", "http://localhost:5209") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    builder.Services.AddControllers();

    builder.Services.AddJWTAuthService(builder.Configuration);

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("VerifiedOnly", policy =>
            policy.RequireClaim("KycStatus", "Verified"));
    });


    builder.Services.AddTransient<IUserRepository, UserRepository>();

    builder.Services.AddScoped<ICreditRepository, CreditRepository>();

    builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();

    builder.Services.AddScoped<IBankAccountService, BankAccountService>();

    builder.Services.AddScoped<IDisbursementRepository, DisbursementRepository>();

    builder.Services.AddScoped<IDisbursementService, DisbursementService>();

    builder.Services.AddScoped<IRepaymentRepository, RepaymentRepository>();

    builder.Services.AddScoped<IRepaymentService, RepaymentService>();

    builder.Services.AddScoped<IKycRepository, KycRepository>();

    builder.Services.AddScoped<IKycService, KycService>();

    builder.Services.AddScoped<ICreditService, CreditService>();

    builder.Services.AddScoped<TokenService>();

    builder.Services.AddTransient<IStaffRepository, StaffRepository>();

    builder.Services.AddScoped<IAdminService, AdminService>();

    builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();


    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "LendFlow API V1");
            c.RoutePrefix = "swagger";
        });
    }


    app.UseMiddleware<ExceptionMiddleware>();

    app.UseCors("AllowSpecificOrigin");

    // 3. Inject the Global Logging Middleware directly into the HTTP pipeline
    app.UseHttpLogging();


    app.UseHttpsRedirection();


    app.UseAuthentication();

    app.UseAuthorization();

    // Enable the server to serve files from wwwroot
    app.UseStaticFiles(
        new StaticFileOptions
        {
            // 1. Where the files actually live on the disk
            FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "Assets")),

            // 2. The URL segment the user will type in the browser
            RequestPath = "/StaticContent"
        });

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of an initialization exception");
    throw;
}
finally
{
    // Clean up and flush logging targets on app shutdown
    LogManager.Shutdown();
}
