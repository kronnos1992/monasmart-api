using Microsoft.Extensions.FileProviders;

namespace Claver.Api.Root;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app,
    IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }
        return app;
    }
    // servicos para usar o cors
    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors(p =>
         p
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod()
            // .AllowCredentials();
        );
        return app;
    }
    // servico para usar o swagger
    public static IApplicationBuilder UseSwaggerRoutes(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(s => {
            s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            s.RoutePrefix = string.Empty;
         });
        return app;
    }

    public static IApplicationBuilder UseAppStaticFiles(this IApplicationBuilder app)
    {
        app.UseStaticFiles(new StaticFileOptions{
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"Arquivos")),
            RequestPath="/Arquivos"
        });
        return app;
    } 
}