
using APID02.MapperConfig;
using APID02.Models;
using APID02.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;


namespace APID02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.WriteIndented = true;
    });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
           
            builder.Services.AddDbContext<ITIContext>(op=>op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("db")));
            builder.Services.AddAutoMapper(typeof(mapconfig));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddAuthentication(op => op.DefaultAuthenticateScheme = "myschema")
                     .AddJwtBearer("myschema", option => {

                         var key = "welcome to my sercert key Afaf Tabana";
                         var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                         option.TokenValidationParameters = new TokenValidationParameters()
                         {
                             IssuerSigningKey = secertkey,
                             ValidateIssuer = false,
                             ValidateAudience = false
                         };


                     });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
           // if (app.Environment.IsDevelopment())
           // {
               
           // }
            app.MapOpenApi();
            app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(txt);

            app.MapControllers();

            app.Run();
        }
    }
}
