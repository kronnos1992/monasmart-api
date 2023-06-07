using Claver.Api.Root;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;


internal class Program
{
    [Obsolete]
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // padrao composition root

        // Add services to the container. - configureServices
        builder.Services.AddEndpointsApiExplorer();

        builder.AddSwagger();
        builder.AddPersistence();

        builder.Services.AddCors(
            c =>
            {
                c.AddPolicy("ReactOrigin", options =>
                    options
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()

                );
            }
        );

        builder.AddAuthentication();
        builder.Services.AddDataProtection().UseCryptographicAlgorithms(
            new AuthenticatedEncryptorConfiguration
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                //ValidationAlgorithm = ValidationAlgorithm.HMACSHA512
            });

        builder.Services.Configure<FormOptions>(o =>
        {
            o.ValueLengthLimit = int.MaxValue;
            o.MultipartBodyLengthLimit = int.MaxValue;
            o.MemoryBufferThreshold = int.MaxValue;
        });

        builder.Services.AddControllers()
            // .AddFluentValidations()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                = new DefaultContractResolver());

        var app = builder.Build();

        // app.AddAppCors()
        app.UseAppCors();

        // tratamento de excepcoes
        var environment = app.Environment;

        app.UseExceptionHandling(environment)
            .UseSwaggerRoutes();


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseRouting();

        // using static files to store
        app.UseAppStaticFiles();

        //Setup local IP\
        // string localIP = IpConfig.LocalIPAddress();

        // app.Urls.Add("http://" + localIP + ":5000");
        // app.Urls.Add("https://" + localIP + ":5001");


        app.Run();
    }
}