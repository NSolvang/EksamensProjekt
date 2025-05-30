using Core;
using EksamensProjekt.Service;
using ServerAPI.Repositories;
namespace ServerAPI;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        
        // Tilføj Swagger services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "ServerAPI", Version = "v1" });
            
            // Inkluder XML kommentarer
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("policy",
                policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
        });
        builder.Services.AddSingleton<IUserRepository, UserRepositoryMongodb>();
        builder.Services.AddSingleton<IStudentplanRepository, StudentplanRepositoryMongodb>();
        builder.Services.AddSingleton<ICommentRepository, CommentRepositoryMongodb>();
        builder.Services.AddSingleton<ILocationRepository, LocationRepositoryMongodb>();
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            
            // Tilføj Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("policy");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}