using System.Reflection;
using System.Text;
using AutoMapper;
using Claver.Aplicacao.Implemetancao;
using Claver.Aplicacao.Mapping;
using Claver.Aplicacao.Validations;
using Claver.Dominio.Contratos.Geral;
using Claver.Dominio.DTOS.InscricaoDTOs;
using Claver.Infrastrutura.Context;
using Claver.Infrastrutura.Repositorio.Geral;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Claver.Api.Root;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwagger();
        return builder;
    }
    
    // configurar o swagger para receber tokens no header 
    public static IServiceCollection AddSwagger(this IServiceCollection _services)
    {
        _services.AddEndpointsApiExplorer();
        _services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo { 
                Title = "claver-api", 
                Version = "v1.0",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Nossos Contactos",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Licença",
                    Url = new Uri("https://example.com/license")
                }
            });

           
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            
            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = @"JWT Authorization using the Bearer Scheme. 
                    Enter 'Bearer' [space].Example:\ 'Bearer 12345abcdef\'",
            });

            s.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return _services;
    }

    // servico de autenticacao do token
    public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder _builder)
        {
            _builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // jwt definitions
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        // configure jwt definitions
                        ValidIssuer = _builder.Configuration["Jwt:Issuer"],
                        ValidAudience = _builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_builder.Configuration["Jwt:Key"]))

                    };
                });
            _builder.Services.AddAuthorization();
            return _builder;
        }

    // servicos de persistencia no bd
    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder _builder)
    {

        // DI ofdatabase providers
         var sqlDockerCnn = _builder.Configuration.GetConnectionString("DevCnnSql");
        // var SqlCloudCnn = _builder.Configuration.GetConnectionString("CloudCnnSql");
        // var pgDockerCnn = _builder.Configuration.GetConnectionString("DevCnnPg");
        // var pgCloudCnn = _builder.Configuration.GetConnectionString("CloudCnnPg");

        _builder.Services.AddDbContext<ClaverDbContext>(options =>
            options.UseSqlServer(sqlDockerCnn)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .EnableThreadSafetyChecks()
        );

        /* _builder.Services.AddDbContext<ClaverDbContext>(options =>
            options.UseNpgsql(pgDockerCnn)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .EnableThreadSafetyChecks()
            );
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);  */


        // injectar serviços do automapper
        var mapConfig = new MapperConfiguration( conf => {
              conf.AddProfile(new MappingProfile());
              conf.AddProfile(new CursoProfile());
        });
        var mapper = mapConfig.CreateMapper();

        // Armazenamento em memoria
        _builder.Services.AddMemoryCache();
        _builder.Services.AddHttpContextAccessor();

        // DI FOR FLUENT VAIDATIONS AND aUTO mAPPER
        // _builder.Services.AddScoped<IValidator<AddCandidatoDTO>, >();
        _builder.Services.AddValidatorsFromAssemblyContaining(typeof(CandidatoValidations));
        _builder.Services.AddValidatorsFromAssemblyContaining(typeof(AddCursoValidation));
        _builder.Services.AddValidatorsFromAssemblyContaining(typeof(AtualizarCursoValidation));
        
        _builder.Services.AddSingleton(mapper);
            
        // DI of repositories        

        _builder.Services.AddTransient<IUnidadeTrabalho, UnidadeTrabalho>();
        _builder.Services.AddScoped(typeof(ImplementarInscricao), typeof(ImplementarInscricao));
        _builder.Services.AddScoped(typeof(ImplementarCurso), typeof(ImplementarCurso));
        _builder.Services.AddScoped(typeof(ImplementarClasse), typeof(ImplementarClasse));
        _builder.Services.AddScoped(typeof(ImplementarSala), typeof(ImplementarSala));
        _builder.Services.AddScoped(typeof(ImplementarPeriodo), typeof(ImplementarPeriodo));
        _builder.Services.AddScoped(typeof(ImplementarTurma), typeof(ImplementarTurma));
        _builder.Services.AddScoped(typeof(ImplementarDisciplina), typeof(ImplementarDisciplina));
        _builder.Services.AddScoped(typeof(ImplementarCursoDisciplina), typeof(ImplementarCursoDisciplina));
        _builder.Services.AddScoped(typeof(ImplementarAnoLectivo), typeof(ImplementarAnoLectivo));
        _builder.Services.AddScoped(typeof(ImplementarMatricula), typeof(ImplementarMatricula));
        _builder.Services.AddScoped(typeof(ImplementarTurmaSala), typeof(ImplementarTurmaSala));
        return _builder;
        }

}